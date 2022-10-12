namespace BookReserveWeb.Models
{
    public class WebBook
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public bool IsReserved { get; set; }

        public WebBook(string name, string authorName, bool isReserved)
        {
            Name = name;
            AuthorName = authorName;
            IsReserved = isReserved;
        }
    }
}
