using LiteDB;

namespace BookReserveWeb
{
    public class DataBaseLiteDB : IDataBase
    {
        private static readonly string name = "ReservingDB.db";

        public ITable<Author> GetAuthors => throw new System.NotImplementedException();

        public ITable<DBBook> GetBooks => throw new System.NotImplementedException();

        public ITable<Reservation> GetReservations => throw new System.NotImplementedException();

        public ITable<Return> GetReturns => throw new System.NotImplementedException();
    }
}
