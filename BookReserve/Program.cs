using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookReserve
{
    internal class Program
    {
        static void Main()
        {
            new DataBaseClearAndFill().ClearAndFill();
            PrintCollection<Author>();
            PrintCollection<Book>();
            Console.WriteLine("\nEnd");
            Console.ReadKey();
        }

        static void PrintCollection<P>() where P : IDataBaseCollection 
        {
            Console.WriteLine("\n" + DataBase.Collections.NamesOfCollections[typeof(P)]);
            DataBase.Act<P>(col => {
                foreach (var item in col.FindAll()) {
                    Console.WriteLine(item);
                }
            });
        }
    }
}
