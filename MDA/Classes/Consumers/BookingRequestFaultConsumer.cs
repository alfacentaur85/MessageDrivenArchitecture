using System;
using System.Threading.Tasks;
using MassTransit;
using MDA.Restaurant.Messages.Interfaces;

namespace MDA.Restaurant.Booking.Classes.Consumers
{
    /// <summary>
    /// BookingRequestFaultConsumer
    /// </summary>
    public class BookingRequestFaultConsumer : IConsumer<Fault<IBookingRequest>>
    {
        public Task Consume(ConsumeContext<Fault<IBookingRequest>> context)
        {
            Console.WriteLine($"[OrderId {context.Message.Message.OrderId}] Отмена в зале");
            return Task.CompletedTask;
        }
    }
}