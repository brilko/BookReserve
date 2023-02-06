using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookReserveWeb
{
    public interface ILibrary
    {
        public ReservationResults ReserveBook(int idBook, string comment);
        public RemovingReservationResults RemoveReservation(int bookId);
        public IEnumerable<WebBook> GetAllReserved();
        public IEnumerable<WebBook> GetAllNotReserved();
        public IActionResult GetStatusHistory(int bookId);
    }
}
