using System;
using MDA.Restaurant.Messages.Interfaces;

namespace MDA.Restaurant.Messages.Classes
{
    /// <summary>
    /// BookingCancellation
    /// </summary>
    public class BookingCancellation : IBookingCancellation
    {
        public BookingCancellation(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
