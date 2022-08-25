using System.Collections;
using System.Collections.Generic;

namespace MDA.Restaurant.Messages.Interfaces
{
    public interface IInMemoryRepository<T> where T : class
    {
        public void AddOrUpdate(T entity);

        public IEnumerable<T> Get();
    }
}