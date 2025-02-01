using System.Collections.Generic;

namespace VampireSquid.Common.Repositories
{
    public interface IRepository<T>
    {
        public IReadOnlyList<T> Items { get; }

        public void Add(T    item);
        public void Remove(T item);
    }
}