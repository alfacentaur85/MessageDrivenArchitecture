using System;

namespace MDA.Restaurant.Messages.Interfaces
{
    public interface IBookingCancellation
    {
        public Guid OrderId { get; }
    }

}

