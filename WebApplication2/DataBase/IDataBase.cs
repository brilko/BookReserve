namespace BookReserveWeb
{
    public interface IDataBase
    {
        public ITable<Author> GetAuthors { get; }
        public ITable<DBBook> GetBooks { get; }
        public ITable<Reservation> GetReservations { get; }
        public ITable<Return> GetReturns { get; }
    }
}
