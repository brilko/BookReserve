using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BookReserveWeb
{
    [ApiController]
    [Route("Book")]
    public class BookReserveController : ControllerBase
    {
        private readonly ILogger<BookReserveController> _logger;
        private static readonly ILibrary library = LibraryFactory.Produce();

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
            return library.RemoveReservation(bookId) switch {
                RemovingReservationResults.ReservationWasRemoved => "Removed reserved status",
                RemovingReservationResults.BookWasNotReserved => "Book is not reserved",
                RemovingReservationResults.BookIsNotExist => "Book is not exist",
                _ => throw new NotImplementedException("Unexpeted answer of removing reservation"),
            };
        }

        [HttpGet("AllReserved")]
        public IEnumerable<WebBook> GetAllReserved()
        {
            return library.GetBooksByReservedStatus(true);
        }

        [HttpGet("AllNotReserved")]
        public IEnumerable<WebBook> GetAllNotReserved()
        {
            return library.GetBooksByReservedStatus(false);
        }

        [HttpGet("StatusHistory")]
        public IActionResult GetStatusHistory(int bookId)
        {
            var statuses = library.GetStatusHistory(bookId);
            return statuses.Item1 switch {
                GetHistoryStatusResult.DataBaseIntegrityViolation => StatusCode(500, "Integrity database"),
                GetHistoryStatusResult.Success => StatusCode(200, statuses.Item2),
                GetHistoryStatusResult.BookIsNotExist => StatusCode(200, "The book isn't exist"),
                _ => throw new NotImplementedException()
            };
        }
    }
}
