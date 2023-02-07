using BookReserveWeb.DataBase;
using BookReserveWeb.DataBase.Interfaces;
using BookReserveWeb.Models.UIModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookReserveWeb
{
    public class Library : ILibrary
    {
        private static readonly IDataBase db = DataBaseFactory.Produce();
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
            var book = db.Books.GetById(idBook);
            if (book == null) 
                return ReservationResults.BookIsNotExist;
            if (book.IsReserved == true) 
                return ReservationResults.AlreadyHadBeenReserved;
            MakeReserationOfBook(book, comment);
            return ReservationResults.Reserved;
        }

        private static void MakeReserationOfBook(DBBook book, string comment) {
            var newReservation = new Reservation(
                book.Id,
                DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(),
                comment);
            db.Reservations.Insert(newReservation);
            book.IsReserved = true;
            db.Books.Update(book);
        }
    }
}
