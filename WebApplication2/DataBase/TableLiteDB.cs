using LiteDB;
using System;
using System.Collections.Generic;

namespace BookReserveWeb
{
    public class TableLiteDB<P> : ITable<P> where P : IDataBaseCollection
    {
        private readonly string dataBaseName;
        public LiteDatabase GetDataBase { get => new(dataBaseName); }
        public TableLiteDB(string dataBaseName)
        {
            this.dataBaseName = dataBaseName;
        }

        private static readonly object locker = default;
        private void TableAct(Action<ILiteCollection<P>> act)
        {
            lock (locker) {
                using var db = GetDataBase;
                var collection = db.GetCollection<P>(typeof(P).Name + 's');
                act(collection);
            }
        }

        public void Delete(P element)
        {
            TableAct(col => col.Delete(element.Id));
        }

        public void DeleteAll()
        {
            TableAct(col => col.DeleteAll());
        }

        public P GetById(int id)
        {
            P element = default;
            TableAct(col => element = col.FindById(id));
            return element;
        }

        public IEnumerable<P> GetMany(Func<P, bool> criteria)
        {
            IEnumerable<P> elements = default;
            TableAct(col => elements = col.Find(el => criteria(el)));
            return elements;
        }

        public void Insert(P element)
        {
            TableAct(col => col.Insert(element));
        }

        public void Update(P element)
        {
            TableAct(col => col.Update(element));
        }
    }
}
