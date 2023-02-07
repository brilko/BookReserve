namespace BookReserveWeb
{
    public class DataBaseLiteDB : IDataBase
    {
        private static readonly string name = "ReservingDB.db";
        private static readonly TableLiteDB<Author> authors = new(name);
        public ITable<Author> GetAuthors => authors;

        private static readonly TableLiteDB<DBBook> books = new(name);
        public ITable<DBBook> GetBooks => books;

        private static readonly TableLiteDB<Reservation> reservations = new(name);
        public ITable<Reservation> GetReservations => reservations;

        private static readonly TableLiteDB<Return> returns = new(name);
        public ITable<Return> GetReturns => returns;
    }
}
