using System;
using System.Collections.Generic;
using System.Linq;

namespace BookReserveWeb
{
    public class Library : ILibrary
    {
        private static readonly IDataBase db = DataBaseFactory.Produce();

        public Tuple<GetHistoryStatusResult, StatusOfBook[]> GetStatusHistory(int bookId)
        {
            if (db.Books.GetById(bookId) == null)
                return new Tuple<GetHistoryStatusResult, StatusOfBook[]>(GetHistoryStatusResult.BookIsNotExist, default);
            var reservations = db.Reservations
                .GetMany(r => r.IdBook == bookId)
                .ToArray();
            var returns = db.Returns
                .GetMany(r => r.IdBook == bookId)
                .ToArray();

            var deltaLength = reservations.Length - returns.Length;
            if (deltaLength < 0 || deltaLength > 1) {
                return new Tuple<GetHistoryStatusResult, StatusOfBook[]>(
                    GetHistoryStatusResult.DataBaseIntegrityViolation,
                    default);
            }
            var statuses = new StatusOfBook[reservations.Length];
            for (var i = 0; i < returns.Length; i++) {
                statuses[i] = new StatusOfBook(reservations[i], returns[i]);
            }
            if (reservations.Length > returns.Length)
                statuses[reservations.Length - 1] = new StatusOfBook(reservations.Last(), null);
            return new Tuple<GetHistoryStatusResult, StatusOfBook[]>(GetHistoryStatusResult.Success, statuses);






            /*
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
            });*/
        }

        public RemovingReservationResults RemoveReservation(int idBook)
        {
            var book = db.Books.GetById(idBook);
            if (book == null)
                return RemovingReservationResults.BookIsNotExist;
            if (book.IsReserved == false)
                return RemovingReservationResults.BookWasNotReserved;
            RemoveReserationOfBook(book);
            return RemovingReservationResults.ReservationWasRemoved;
        }

        private static void RemoveReserationOfBook(DBBook book)
        {
            var newReturn = new Return(
                book.Id,
                DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
            db.Returns.Insert(newReturn);
            book.IsReserved = false;
            db.Books.Update(book);
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

        private static void MakeReserationOfBook(DBBook book, string comment)
        {
            var newReservation = new Reservation(
                book.Id,
                DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(),
                comment);
            db.Reservations.Insert(newReservation);
            book.IsReserved = true;
            db.Books.Update(book);
        }

        public IEnumerable<WebBook> GetBooksByReservedStatus(bool reservedStatus)
        {
            return db.Books
                .GetMany(b => b.IsReserved == reservedStatus)
                .Select(dbBook => new WebBook(dbBook))
                .ToArray();
        }
    }
}
