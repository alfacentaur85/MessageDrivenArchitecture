using System;
using MDA.Restaurant.Messages.Enums;

namespace MDA.Restaurant.Messages.Interfaces
{
    public interface IKitchenAccident
    {
        public Guid OrderId { get; }

        public Dish Dish { get; }
    }
}
