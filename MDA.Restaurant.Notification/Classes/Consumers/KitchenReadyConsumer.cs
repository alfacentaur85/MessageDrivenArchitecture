using System.Threading.Tasks;
using MassTransit;
using MDA.Restaurant.Messages.Interfaces;
using MDA.Restaurant.Notification.Enums;

namespace MDA.Restaurant.Notification.Classes.Consumers
{
    public class KitchenReadyConsumer : IConsumer<IKitchenReady>
    {

        private readonly Notifier _notifier;

        public KitchenReadyConsumer(Notifier notifier)
        {
            _notifier = notifier;
        }

        public Task Consume(ConsumeContext<IKitchenReady> context)
        {
            _notifier.Accept(context.Message.OrderId, Accepted.Kitchen);

            return Task.CompletedTask;
        }
    }
}