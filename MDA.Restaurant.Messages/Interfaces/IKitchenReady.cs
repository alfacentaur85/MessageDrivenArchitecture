using System;

namespace MDA.Restaurant.Messages.Interfaces
{
    /// <summary>
    /// IKitchenReady
    /// </summary>
    public interface IKitchenReady
    {
        public Guid OrderId { get; }

        public bool Ready { get; }
    }
}
