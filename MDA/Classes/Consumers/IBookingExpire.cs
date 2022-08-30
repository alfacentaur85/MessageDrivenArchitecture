using System;

namespace MDA.Restaurant.Booking.Classes.Consumers
{
    /// <summary>
    /// IBookingExpire
    /// </summary>
    public interface IBookingExpire
    {
        public Guid OrderId { get; }
    }

    
}