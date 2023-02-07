using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookReserveWeb.Models
{
    public class Library : ILibrary
    {
        public IEnumerable<WebBook> GetAllNotReserved()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<WebBook> GetAllReserved()
        {
            throw new System.NotImplementedException();
        }

        public IActionResult GetStatusHistory(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public RemovingReservationResults RemoveReservation(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public ReservationResults ReserveBook(int idBook, string comment)
        {
            throw new System.NotImplementedException();
        }
    }
}
