using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReserveWeb
{
    public class Return : IDataBaseCollection
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string DateReturn { get; set; }
        public Return(int idBook, string dateReturn)
        {
            IdBook = idBook;
            DateReturn = dateReturn;
        }
        public override string ToString()
        {
            return Id + "\t\t" + IdBook + "\t\t" + DateReturn;
        }
    }
}
