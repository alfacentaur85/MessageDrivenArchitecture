using System;
using MDA.Restaurant.Booking.Classes;

namespace MDA.Restaurant.Booking.Classes.Consumers
{
    public interface IBookingExpire
    {
        public Guid OrderId { get; }
    }

    
}