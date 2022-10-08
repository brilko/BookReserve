using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReserve
{
    internal class Author : IDataBaseCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Author(string name) {
            Name = name;
        }

        public override string ToString()
        {
            return Id + "\t\t" + Name;
        }
    }
}
