﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReserveWeb
{
    public class Reservation : IDataBaseCollection
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string ReservationDate { get; set; }
        public string Comment { get; set; }
        public Reservation(int idBook, string reservationTime, string comment)
        {
            IdBook = idBook;
            ReservationDate = reservationTime;
            Comment = comment;
        }
        public override string ToString()
        {
            return Id + "\t\t" + IdBook + "\t\t" + ReservationDate + "\t\t" + Comment;
        }
    }
}
