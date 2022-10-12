using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReserveWeb
{
    public class DataBaseClearAndFill
    {
        public void ClearAndFill()
        {
            ClearAndFeelCollection(new Author[] {
                new Author("Tolstoy"),
                new Author("Pushkin"),
                new Author("Esenin")
            });
            ClearAndFeelCollection(new DBBook[] { 
                new DBBook("WarAndPeace", 1),
                new DBBook("Anna Karenina", 1),
                new DBBook("Ruslan i Ludmila", 2),
                new DBBook("Evgeniy Onegin", 2),
                new DBBook("I don't pity, don't call, don't cry", 3),
                new DBBook("A Letter to Mother", 3)
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
