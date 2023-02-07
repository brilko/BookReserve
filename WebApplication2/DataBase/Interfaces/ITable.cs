using System;
using System.Collections.Generic;

namespace BookReserveWeb
{
    public interface ITable<P> where P : IDataBaseCollection
    {
        public void Insert(P element);
        public void Update(P element);
        public void Delete(P element);
        public void DeleteAll();
        public P GetById(int id);
        public IEnumerable<P> GetMany(Func<P, bool> criteria);
    }
}
