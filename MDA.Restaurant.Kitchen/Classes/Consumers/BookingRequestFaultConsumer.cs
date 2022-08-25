using System;
using System.Threading.Tasks;
using MassTransit;
using MDA.Restaurant.Messages.Interfaces;

namespace MDA.Restaurant.Kitchen.Classes.Consumers
{
    public class KitchenBookingRequestFaultConsumer : IConsumer<Fault<IBookingRequest>>
    {
        public Task Consume(ConsumeContext<Fault<IBookingRequest>> context)
        {
            //Console.WriteLine($"[OrderId {context.Message.Message.OrderId}] Отмена на кухне");
            return Task.CompletedTask;
        }
    }
}