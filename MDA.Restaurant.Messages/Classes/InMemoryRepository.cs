using MDA.Restaurant.Messages.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace MDA.Restaurant.Messages.Classes
{
    /// <summary>
    /// InMemoryRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InMemoryRepository<T> : IInMemoryRepository<T> where T : class
    {
        private readonly ConcurrentBag<T> _repo = new ();

        public void AddOrUpdate(T entity)
        {
            _repo.Add(entity);
        }

        public IEnumerable<T> Get()
        {
            return _repo;
        }
    }
}