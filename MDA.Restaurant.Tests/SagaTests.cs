﻿using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using MDA.Restaurant.Booking.Classes.Consumers;
using MDA.Restaurant.Messages.Classes;
using MDA.Restaurant.Booking.Classes;
using MDA.Restaurant.Kitchen.Classes;
using MDA.Restaurant.Messages.Interfaces;
using MDA.Restaurant.Kitchen.Classes.Consumers;

namespace MDA.Restaurant.Tests
{
    [TestFixture]
    public class SagaTests
    {
        [OneTimeSetUp]
        public async Task Init()
        {
            _provider = new ServiceCollection()
                .AddMassTransitInMemoryTestHarness(cfg =>
                {
                    cfg.AddConsumer<KitchenBookingRequestedConsumer>();
                    cfg.AddConsumer<RestaurantBookingRequestConsumer>();

                    cfg.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                        .InMemoryRepository();
                    cfg.AddSagaStateMachineTestHarness<RestaurantBookingSaga, RestaurantBooking>();
                    cfg.AddDelayedMessageScheduler();
                })
                .AddLogging()
                .AddTransient<MDA.Restaurant.Booking.Classes.Restaurant>()
                .AddTransient<Manager>()
                .AddSingleton<IInMemoryRepository<IBookingRequest>, InMemoryRepository<IBookingRequest>>()
                .BuildServiceProvider(true);

            _harness = _provider.GetRequiredService<InMemoryTestHarness>();

            _harness.OnConfigureInMemoryBus += configurator => configurator.UseDelayedMessageScheduler();

            await _harness.Start();
        }

        private ServiceProvider _provider;
        private InMemoryTestHarness _harness;

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _harness.Stop();
            await _provider.DisposeAsync();
        }

        [Test]
        public async Task Should_be_easy()
        {
            var orderId = NewId.NextGuid();
            var clientId = NewId.NextGuid();

            await _harness.Bus.Publish(new BookingRequest(orderId,
                clientId,
                null,
                DateTime.Now));

            Assert.That(await _harness.Published.Any<IBookingRequest>());
            Assert.That(await _harness.Consumed.Any<IBookingRequest>());

            var sagaHarness = _provider
                .GetRequiredService<ISagaStateMachineTestHarness<RestaurantBookingSaga, RestaurantBooking>>();

            Assert.That(await sagaHarness.Consumed.Any<IBookingRequest>());
            Assert.That(await sagaHarness.Created.Any(x => x.CorrelationId == orderId));

            var saga = sagaHarness.Created.Contains(orderId);

            Assert.That(saga, Is.Not.Null);
            Assert.That(saga.ClientId, Is.EqualTo(clientId));
            Assert.That(await _harness.Published.Any<ITableBooked>());

            Assert.That(await _harness.Published.Any<INotify>());
            Assert.That(saga.CurrentState, Is.EqualTo(3));

            await _harness.OutputTimeline(TestContext.Out, options => options.Now().IncludeAddress());
        }
    }
}

