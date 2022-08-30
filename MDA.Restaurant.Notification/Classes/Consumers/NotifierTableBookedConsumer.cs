using System.Threading.Tasks;
using MassTransit;
using MDA.Restaurant.Notification.Enums;
using MDA.Restaurant.Messages.Interfaces;

namespace MDA.Restaurant.Notification.Classes.Consumers
{
    /// <summary>
    /// NotifierTableBookedConsumer
    /// </summary>
    public class NotifierTableBookedConsumer : IConsumer<ITableBooked>
    {
        private readonly Notifier _notifier;

        public NotifierTableBookedConsumer(Notifier notifier)
        {
            _notifier = notifier;
        }

        public  Task Consume(ConsumeContext<ITableBooked> context)
        {
           var result = context.Message.Success;

           _notifier.Accept(context.Message.OrderId, result ? Accepted.Booking : Accepted.Rejected,
               context.Message.ClientId);

           return Task.CompletedTask;
        }
    }
}