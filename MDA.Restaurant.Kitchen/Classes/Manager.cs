using System;
using MassTransit;
using MDA.Restaurant.Messages.Enums;
using MDA.Restaurant.Messages.Interfaces;
using MDA.Restaurant.Messages.Classes;

namespace MDA.Restaurant.Kitchen.Classes
{
    internal class Manager
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