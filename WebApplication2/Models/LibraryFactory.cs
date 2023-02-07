namespace BookReserveWeb
{
    public static class LibraryFactory
    {
        public static ILibrary Produce() => new Library();
    }
}
