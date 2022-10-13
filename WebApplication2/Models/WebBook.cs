using LiteDB;
using System.Linq;
using System.Security.Policy;

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

        public WebBook(DBBook dbBook, LiteDatabase db) 
        {
            var authors = DataBase.GetCollection<Author>(db).FindAll();
            IdBook = dbBook.Id;
            BookName = dbBook.Name;
            AuthorName = authors.Where(a => a.Id == dbBook.IdAuthor).First().Name;
            IsReserved = dbBook.IsReserved;
        }
    }
}
