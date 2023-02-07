using BookReserveWeb.Models.UIModels;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookReserveWeb
{
    [ApiController]
    [Route("Book")]
    public class BookReserveController : ControllerBase
    {
        private readonly ILogger<BookReserveController> _logger;
        private readonly ILibrary library = LibraryFactory.Produce();

        public BookReserveController(ILogger<BookReserveController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Reserve")]
        public string ReserveBook(int idBook, string comment)
        {
            return library.ReserveBook(idBook, comment) switch {
                ReservationResults.Reserved => "Reserved",
                ReservationResults.AlreadyHadBeenReserved => "you didn't reserve the book because the book was already reserved",
                ReservationResults.BookIsNotExist => "The book isn't existed",
                _ => throw new NotImplementedException("Unexpeted answer of reservation"),
            };
        }

        [HttpPost("RemoveReserve")]
        public string RemoveReservedStatus(int bookId)
        {
            var isBookExist = true;
            var isRemovedReserved = true;
            DataBaseBad.DBAct((db) => {
                var booksCollection = DataBaseBad.GetCollection<DBBook>(db);
                var book = booksCollection.FindById(bookId);
                if (book == null) {
                    isBookExist = false;
                    return;
                }
                if (book.IsReserved == true) {
                    isRemovedReserved = true;
                    book.IsReserved = false;
                    booksCollection.Update(book);
                    DataBaseBad.GetCollection<Return>(db).Insert(
                        new Return(bookId, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString()));
                } else {
                    isRemovedReserved = false;
                }
            });
            if (isBookExist == false) return "Book is not exist";
            return isRemovedReserved ? "Removed reserved status" : "Book is not reserved";
        }

        [HttpGet("AllReserved")]
        public IEnumerable<WebBook> GetAllReserved()
        {
            return GetBooksByReservedStatus(true);
        }

        [HttpGet("AllNotReserved")]
        public IEnumerable<WebBook> GetAllNotReserved()
        {
            return GetBooksByReservedStatus(false);
        }

        private WebBook[] GetBooksByReservedStatus(bool reservedStatus)
        {
            WebBook[] books = new WebBook[0];
            DataBaseBad.DBAct((db) => {
                books = DataBaseBad.GetCollection<DBBook>(db).FindAll()
                    .Where(b => b.IsReserved == reservedStatus)
                    .Select(dbBook => new WebBook(dbBook, db))
                    .ToArray();
            });
            return books;
        }

        [HttpGet("StatusHistory")]
        public IActionResult GetStatusHistory(int bookId)
        {
            StatusOfBook[] statuses = Array.Empty<StatusOfBook>();
            var isDatabaseIntegrity = true;
            DataBaseBad.DBAct(db => {
                var reservations = DataBaseBad.GetCollection<Reservation>(db).FindAll()
                    .Where(r => r.IdBook == bookId)
                    .ToArray();
                var returns = DataBaseBad.GetCollection<Return>(db).FindAll()
                    .Where((r) => r.IdBook == bookId)
                    .ToArray();

                var deltaLength = reservations.Length - returns.Length;
                if (deltaLength < 0 || deltaLength > 1) {
                    isDatabaseIntegrity = false;
                    return;
                }
                statuses = new StatusOfBook[reservations.Length];
                for (var i = 0; i < returns.Length; i++) {
                    statuses[i] = new StatusOfBook(reservations[i], returns[i]);
                }
                if (reservations.Length > returns.Length)
                    statuses[reservations.Length - 1] = new StatusOfBook(reservations.Last(), null);
            });

            if (isDatabaseIntegrity == false)
                return StatusCode(500, "Integrity database");

            return StatusCode(200, statuses);
        }
    }
}
