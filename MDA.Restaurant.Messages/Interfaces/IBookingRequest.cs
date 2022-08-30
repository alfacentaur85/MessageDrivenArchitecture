﻿using System;
using MDA.Restaurant.Messages.Enums;

namespace MDA.Restaurant.Messages.Interfaces
{
    /// <summary>
    /// IBookingRequest
    /// </summary>
    public interface IBookingRequest
    {
        public Guid OrderId { get; }
        
        public Guid ClientId { get; }
        
        public Dish? PreOrder { get; }
        
        public DateTime CreationDate { get; }
    }

}