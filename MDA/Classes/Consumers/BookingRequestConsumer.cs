using System;
using System.Threading.Tasks;
using MassTransit;
using MDA.Restaurant.Messages.Interfaces;

namespace MDA.Restaurant.Booking.Classes.Consumers
{
    public class RestaurantBookingRequestConsumer : IConsumer<IBookingRequest>
    {
        private readonly MDA.Restaurant.Booking.Classes.Restaurant _restaurant;

        public RestaurantBookingRequestConsumer(MDA.Restaurant.Booking.Classes.Restaurant restaurant)
        {
            _restaurant = restaurant;
        }

        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {
            Console.WriteLine($"[OrderId: {context.Message.OrderId}]");
            
        }
    }
}