using System;
using System.Collections.Generic;

namespace BookReserveWeb
{
    public interface ILibrary
    {
        public ReservationResults ReserveBook(int idBook, string comment);
        public RemovingReservationResults RemoveReservation(int bookId);
        public IEnumerable<WebBook> GetBooksByReservedStatus(bool reservedStatus);
        public Tuple<GetHistoryStatusResult, StatusOfBook[]> GetStatusHistory(int bookId);
    }
}
