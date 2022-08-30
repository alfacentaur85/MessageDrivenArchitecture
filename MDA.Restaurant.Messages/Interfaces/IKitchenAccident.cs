using System;
using MDA.Restaurant.Messages.Enums;

namespace MDA.Restaurant.Messages.Interfaces
{
    /// <summary>
    /// IKitchenAccident
    /// </summary>
    public interface IKitchenAccident
    {
        public Guid OrderId { get; }

        public Dish Dish { get; }
    }
}
