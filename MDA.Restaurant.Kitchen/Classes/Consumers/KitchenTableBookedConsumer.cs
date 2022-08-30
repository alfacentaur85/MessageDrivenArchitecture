using System.Threading.Tasks;
using MassTransit;
using MDA.Restaurant.Messages.Interfaces;
using MDA.Restaurant.Messages.Classes;

namespace MDA.Restaurant.Kitchen.Classes.Consumers
{
    /// <summary>
    /// KitchenBookingRequestedConsumer
    /// </summary>
    public class KitchenBookingRequestedConsumer : IConsumer<IBookingRequest>
    {
        private readonly Manager _manager;
        private readonly IBus _bus;

        public KitchenBookingRequestedConsumer(Manager manager, IBus bus)
        {
            _manager = manager;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {

            await Task.Delay(500000);
            if (_manager.CheckKitchenReady(context.Message.OrderId, context.Message.PreOrder))
                await _bus.Publish<IKitchenReady>(new KitchenReady(context.Message.OrderId, true));
        }
    }
}