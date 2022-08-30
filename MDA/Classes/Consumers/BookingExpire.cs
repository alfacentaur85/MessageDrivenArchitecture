using System;

namespace MDA.Restaurant.Booking.Classes.Consumers
{
    /// <summary>
    /// BookingExpire
    /// </summary>
    public class BookingExpire : IBookingExpire
    {
        private readonly RestaurantBooking _instance;

        public BookingExpire(RestaurantBooking instance)
        {
            _instance = instance;
        }

        public Guid OrderId => _instance.OrderId;
    }
}
