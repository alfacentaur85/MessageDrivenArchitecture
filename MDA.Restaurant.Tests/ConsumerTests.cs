using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using MDA.Restaurant.Booking.Classes.Consumers;
using MDA.Restaurant.Messages.Classes;
using MDA.Restaurant.Messages.Interfaces;
using System.Linq;

namespace MDA.Restaurant.Tests
{
    [TestFixture]
    public class ConsumerTests
    {
        private ServiceProvider _provider;
        private ITestHarness _harness;

        [OneTimeSetUp]
        public async Task Init()
        {
            _provider = new ServiceCollection()
                .AddMassTransitTestHarness(cfg =>
                {
                    cfg.AddConsumer<RestaurantBookingRequestConsumer>();
                })
                .AddLogging()
                .AddTransient<MDA.Restaurant.Booking.Classes.Restaurant>()
                .AddSingleton<IInMemoryRepository<IBookingRequest>, InMemoryRepository<IBookingRequest>>()
                .BuildServiceProvider(true);

            _harness = _provider.GetTestHarness();

            await _harness.Start();
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _harness.OutputTimeline(TestContext.Out, options => options.Now().IncludeAddress());
            await _provider.DisposeAsync();
        }


        [Test]
        public async Task Any_booking_request_consumed()
        {
            var orderIdd = Guid.NewGuid();

            await _harness.Bus.Publish(
                (IBookingRequest)new BookingRequest(
                    orderIdd,
                    Guid.NewGuid(),
                    null,
                    DateTime.Now));

            Assert.That(await _harness.Consumed.Any<IBookingRequest>());
        }

        [Test]
        public async Task Booking_request_consumer_published_table_booked_message()
        {
            var consumer = _harness.GetConsumerHarness<RestaurantBookingRequestConsumer>();

            var orderId = NewId.NextGuid();
            var bus = _harness.Bus;

            await bus.Publish((IBookingRequest)
                new BookingRequest(orderId,
                    orderId,
                    null,
                    DateTime.Now));

            Assert.That(consumer.Consumed.Select<IBookingRequest>()
                .Any(x => x.Context.Message.OrderId == orderId), Is.True);
            var selected = _harness.Published.Select<ITableBooked>().FirstOrDefault();


            Assert.That(_harness.Published.Select<ITableBooked>()
                .Any(x => x.Context.Message.OrderId == orderId), Is.True);
        }
    }
}

