using System;

namespace BookReserveWeb
{
    public class StatusOfBook
    {
        public string ReservationDate { get; set; }
        public string ReturnDate { get; set; }
        public string Comment { get; set; }

        public StatusOfBook(Reservation reservation, Return returning) { 
            ReservationDate = reservation.ReservationDate;
            ReturnDate = returning==null?"No return":returning.DateReturn;
            Comment = reservation.Comment;
        }
    }
}
