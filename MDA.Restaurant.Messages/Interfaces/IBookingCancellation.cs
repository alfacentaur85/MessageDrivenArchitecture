using System;

namespace MDA.Restaurant.Messages.Interfaces
{
    /// <summary>
    /// IBookingCancellation
    /// </summary>
    public interface IBookingCancellation
    {
        public Guid OrderId { get; }
    }

}

