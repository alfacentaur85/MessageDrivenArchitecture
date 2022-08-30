using System;
using MDA.Restaurant.Messages.Enums;

namespace MDA.Restaurant.Messages.Interfaces
{
    /// <summary>
    /// ITableBooked
    /// </summary>
    public interface ITableBooked
    {
        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public bool Success { get; }

        public DateTime CreationDate { get; }
    }
}
