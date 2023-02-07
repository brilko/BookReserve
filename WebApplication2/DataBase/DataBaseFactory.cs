using BookReserveWeb.DataBase.Interfaces;

namespace BookReserveWeb.DataBase
{
    public static class DataBaseFactory
    {
        public static IDataBase Produce() => new DataBaseLiteDB();
    }
}
