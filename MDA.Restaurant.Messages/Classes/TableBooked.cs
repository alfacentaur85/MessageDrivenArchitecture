﻿using System;
using MDA.Restaurant.Messages.Enums;
using MDA.Restaurant.Messages.Interfaces;

namespace MDA.Restaurant.Messages.Classes
{
    /// <summary>
    /// TableBooked
    /// </summary>
    public class TableBooked : ITableBooked
    {

        public TableBooked(Guid orderId, Guid clientId, bool success, Dish? preOrder = null)
        {
            OrderId = orderId;
            ClientId = clientId;
            Success = success;
            PreOrder = preOrder;
        }

        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public Dish? PreOrder { get; }
        public bool Success { get; }
        public DateTime CreationDate { get; }
    }
}