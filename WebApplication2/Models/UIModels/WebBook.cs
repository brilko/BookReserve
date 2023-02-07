namespace BookReserveWeb
{
    public class WebBook
    {
        public int IdBook { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public bool IsReserved { get; set; }

        public WebBook(int idBook, string bookName, string authorName, bool isReserved)
        {
            IdBook = idBook;
            BookName = bookName;
            AuthorName = authorName;
            IsReserved = isReserved;
        }

        public WebBook(DBBook dbBook)
        {
            var db = DataBaseFactory.Produce();
            IdBook = dbBook.Id;
            BookName = dbBook.Name;
            AuthorName = db.Authors.GetById(dbBook.IdAuthor).Name;
            IsReserved = dbBook.IsReserved;
        }
    }
}
