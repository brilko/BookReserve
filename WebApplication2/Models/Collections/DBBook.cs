namespace BookReserveWeb
{
    public class DBBook : IDataBaseCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdAuthor { get; set; }
        public bool IsReserved { get; set; }

        public DBBook(string name, int idAuthor) { 
            Name = name;
            IdAuthor = idAuthor;
        }

        public override string ToString()
        {
            return Id + "\t\t" + Name + "\t\t" + IdAuthor + "\t\t" + IsReserved;
        }
    }
}
