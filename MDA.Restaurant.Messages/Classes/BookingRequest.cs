using MDA.Restaurant.Messages.Enums;
using MDA.Restaurant.Messages.Interfaces;
using System;


namespace MDA.Restaurant.Messages.Classes
{
    public class BookingRequest : IBookingRequest
    {
        public BookingRequest(Guid orderId, Guid clientId, Dish? preOrder, DateTime creationDate)
        {
            OrderId = orderId;
            ClientId = clientId;
            PreOrder = preOrder;
            CreationDate = creationDate;
        }

        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public Dish? PreOrder { get; }

        public DateTime CreationDate { get; }
    }
}
