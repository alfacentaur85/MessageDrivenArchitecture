using System.Threading.Tasks;
using MassTransit;
using MDA.Restaurant.Messages.Interfaces;

namespace MDA.Restaurant.Kitchen.Classes.Consumers
{
    /// <summary>
    /// KitchenBookingRequestFaultConsumer
    /// </summary>
    public class KitchenBookingRequestFaultConsumer : IConsumer<Fault<IBookingRequest>>
    {
        public Task Consume(ConsumeContext<Fault<IBookingRequest>> context)
        {
            return Task.CompletedTask;
        }
    }
}