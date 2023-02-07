namespace BookReserveWeb
{
    public static class DataBaseFactory
    {
        private static readonly IDataBase dataBase = new DataBaseLiteDB();
        public static IDataBase Produce() => dataBase;
    }
}
