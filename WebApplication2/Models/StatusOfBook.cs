using System;

namespace BookReserveWeb
{
    public class StatusOfBook
    {
        public DateTime ReservationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Comment { get; set; }
    }
}
