using System;
using MassTransit;
using MDA.Restaurant.Messages.Enums;

namespace MDA.Restaurant.Kitchen.Classes
{
    /// <summary>
    /// Manager
    /// </summary>
    public class Manager
    {
        private readonly IBus _bus;

        public Manager(IBus bus)
        {
            _bus = bus;
        }

        public bool CheckKitchenReady(Guid orderId, Dish? dish)
        {
            return true;
        }
    }
}