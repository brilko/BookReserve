namespace BookReserveWeb.DataBase.Interfaces
{
    public interface IDataBase
    {
        public ITable<Author> Authors { get; }
        public ITable<DBBook> Books { get; }
        public ITable<Reservation> Reservations { get; }
        public ITable<Return> Returns { get; }
    }
}
