using System.Threading.Tasks;
using MassTransit;
using MDA.Restaurant.Messages.Interfaces;
using System;
using MDA.Restaurant.Messages.Classes;

namespace MDA.Restaurant.Kitchen.Classes.Consumers
{
    internal class KitchenTableBookedConsumer : IConsumer<ITableBooked>
    {
        private readonly Manager _manager;

        public KitchenTableBookedConsumer(Manager manager)
        {
            _manager = manager;
        }

        public async Task Consume(ConsumeContext<ITableBooked> context)
        {
            Console.WriteLine($"[OrderId: {context.Message.OrderId} CreationDate: {context.Message.CreationDate}]");
            Console.WriteLine("Trying time: " + DateTime.Now);

            await Task.Delay(5000);

            if (_manager.CheckKitchenReady(context.Message.OrderId, context.Message.PreOrder))
                await context.Publish<IKitchenReady>(new KitchenReady(context.Message.OrderId, true));
        }
    }
}