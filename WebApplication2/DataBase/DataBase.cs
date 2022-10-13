using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace BookReserveWeb
{
    public static class DataBase
    {
        public static string Name { get => @"ReservingDB.db"; }
        public static readonly CollectionsClass Collections = new();

        public static LiteDatabase GetDataBase { get => new(Name); }
        public static void ActCollection<P>(string collectionName, Action<ILiteCollection<P>> act)
        {
            using (var db = GetDataBase) {
                act(db.GetCollection<P>(collectionName));
            }
        }

        public static void Act<P>(Action<ILiteCollection<P>> act) where P : IDataBaseCollection
        {
            using (var db = GetDataBase) {
                act(db.GetCollection<P>(Collections.NamesOfCollections[typeof(P)]));
            }
        }

        public static void DBAct(Action<LiteDatabase> act)
        {
            using (var db = GetDataBase) 
            {
                act(db); 
            }
        }

        public static ILiteCollection<P> GetCollection<P>(LiteDatabase db) where P : IDataBaseCollection 
        {
            return db.GetCollection<P>(Collections.NamesOfCollections[typeof(P)]);
        }

        public class CollectionsClass
        {
            public string Authors { get => "Authors"; }
            public string Books { get => "Books"; }
            public string Reservations { get => "Reservations"; }
            public string Returns { get => "Returns"; }

            public Dictionary<Type, string> NamesOfCollections = new Dictionary<Type, string>() {
                [typeof(Author)] = "Authors",
                [typeof(DBBook)] = "Books",
                [typeof(Reservation)] = "Reservations",
                [typeof(Return)] = "Returns"
            };
        }
    }
}
