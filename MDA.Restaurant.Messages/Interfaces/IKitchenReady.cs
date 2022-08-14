using System;

namespace MDA.Restaurant.Messages.Interfaces
{
    public interface IKitchenReady
    {
        public Guid OrderId { get; }

        public bool Ready { get; }
    }
}
