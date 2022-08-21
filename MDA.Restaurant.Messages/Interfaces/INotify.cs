using System;

namespace MDA.Restaurant.Messages.Interfaces
{
    public interface INotify
    {
        public Guid OrderId { get; }
        
        public Guid ClientId { get; }
        
        public string Message { get; }
    }
    
}