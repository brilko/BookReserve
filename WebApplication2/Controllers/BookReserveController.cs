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

        public BookReserveController(ILogger<BookReserveController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Reserve")]
        public string ReserveBook(int idBook, string comment)
        {
            var isExist = true;
            var isReserved = false;
            DataBase.DBAct(db => {
                var bookCol = DataBase.GetCollection<DBBook>(db);
                var dbBook = bookCol.FindById(idBook);
                if (dbBook == null) {
                    isExist = false;
                    return;
                }
                if (dbBook.IsReserved == true) {
                    isReserved = false;
                } else {
                    isReserved = true;
                    DataBase.GetCollection<Reservation>(db).
                        Insert(new Reservation(idBook, 
                            DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), comment));
                    dbBook.IsReserved = true;
                    bookCol.Update(dbBook);
                }
            });

            if (isExist == false) return "Book is not exist";
            return isReserved ? "Reserved" : "You don`t reserve this book, because book is already reserved";
        }

        [HttpPost("RemoveReserve")]
        public string RemoveReservedStatus(int bookId)
        {
            var isBookExist = true;
            var isRemovedReserved = true;
            DataBase.DBAct((db) => {
                var booksCollection = DataBase.GetCollection<DBBook>(db);
                var book = booksCollection.FindById(bookId);
                if (book == null) {
                    isBookExist = false;
                    return;
                }
                if (book.IsReserved == true) {
                    isRemovedReserved = true;
                    book.IsReserved = false;
                    booksCollection.Update(book);
                    DataBase.GetCollection<Return>(db).Insert(
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
            DataBase.DBAct((db) => {
                books = DataBase.GetCollection<DBBook>(db).FindAll()
                    .Where(b => b.IsReserved == reservedStatus)
                    .Select(dbBook => new WebBook(dbBook, db))
                    .ToArray();
            });
            return books;
        }

        [HttpGet("StatusHistory")]
        public IActionResult GetStatusHistory(int bookId)
        {
            StatusOfBook[] statuses = new StatusOfBook[0];
            var isDatabaseIntegrity = true;
            DataBase.DBAct(db => {
                var reservations = DataBase.GetCollection<Reservation>(db).FindAll()
                    .Where(r => r.IdBook == bookId)
                    .ToArray();
                var returns = DataBase.GetCollection<Return>(db).FindAll()
                    .Where((r) => r.IdBook == bookId)
                    .ToArray();

                switch (reservations.Length - returns.Length) {
                    case 0:
                    case 1:
                        break;
                    default:
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
