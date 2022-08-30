using System;

namespace MDA.Restaurant.Messages.Interfaces
{
    /// <summary>
    /// INotify
    /// </summary>
    public interface INotify
    {
        public Guid OrderId { get; }
        
        public Guid ClientId { get; }
        
        public string Message { get; }
    }
    
}