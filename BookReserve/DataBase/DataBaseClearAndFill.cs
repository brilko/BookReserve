using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReserve
{
    internal class DataBaseClearAndFill
    {
        public void ClearAndFill()
        {
            ClearAndFeelCollection(new Author[] {
                new Author("Tolstoy"),
                new Author("Pushkin"),
                new Author("Esenin")
            });
            ClearAndFeelCollection(new Book[] { 
                new Book("WarAndPeace", 1),
                new Book("Anna Karenina", 1),
                new Book("Ruslan i Ludmila", 2),
                new Book("Evgeniy Onegin", 2),
                new Book("I don't pity, don't call, don't cry", 3),
                new Book("A Letter to Mother", 3)
            });
        }

        private void ClearAndFeelCollection<P>(P[] newData) where P : IDataBaseCollection
        {
            DataBase.Act<P>(col => {
                col.DeleteAll();
                col.Insert(newData);
            });
        }
    }
}
