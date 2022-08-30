using System.Collections.Generic;

namespace MDA.Restaurant.Messages.Interfaces
{
    /// <summary>
    /// IInMemoryRepository<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInMemoryRepository<T> where T : class
    {
        public void AddOrUpdate(T entity);

        public IEnumerable<T> Get();
    }
}