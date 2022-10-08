using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReserve
{
    internal class Return : IDataBaseCollection
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public DateTime DateReturn { get; set; }
        public Return(int idBook, DateTime dateReturn) { 
            IdBook = idBook;
            DateReturn = dateReturn;
        }
        public override string ToString()
        {
            return Id + "\t\t" + IdBook + "\t\t" + DateReturn;
        }
    }
}
