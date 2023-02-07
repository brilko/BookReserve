namespace BookReserveWeb
{
    public class DataBaseLiteDB : IDataBase
    {
        private static readonly string name = "ReservingDB.db";
        private static readonly TableLiteDB<Author> authors = new(name);
        public ITable<Author> Authors => authors;

        private static readonly TableLiteDB<DBBook> books = new(name);
        public ITable<DBBook> Books => books;

        private static readonly TableLiteDB<Reservation> reservations = new(name);
        public ITable<Reservation> Reservations => reservations;

        private static readonly TableLiteDB<Return> returns = new(name);
        public ITable<Return> Returns => returns;
    }
}
