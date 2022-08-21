﻿using System;
using MDA.Restaurant.Booking.Enums;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Authentication;
using MDA.Restaurant.Booking.Classes.Consumers;

namespace MDA.Restaurant.Booking.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("beaver-01.rmq.cloudamqp.com", 5671, "xqchhxvp", h =>
                {
                    h.Username("xqchhxvp");
                    h.Password("t57uK3jDXaJQgGDgm7OMDuoCAKDIXc9y");

                    h.UseSsl(s =>
                    {
                        s.Protocol = SslProtocols.Tls12;
                    });
                });
            });
            

            CreateHostBuilder(args).Build().Run();
              
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                        {
                            cfg.Host("beaver-01.rmq.cloudamqp.com", 5671, "xqchhxvp", h =>
                            {
                                h.Username("xqchhxvp");
                                h.Password("t57uK3jDXaJQgGDgm7OMDuoCAKDIXc9y");
                                h.UseSsl(s =>
                                {
                                    s.Protocol = SslProtocols.Tls12;
                                });
                            });                          
                        }));

                        x.AddConsumer<RestaurantBookingRequestConsumer>()
                           .Endpoint(e =>
                           {
                               e.Temporary = true;
                           });

                        x.AddConsumer<BookingRequestFaultConsumer>()
                            .Endpoint(e =>
                            {
                                e.Temporary = true;
                            });

                        x.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                            .Endpoint(e => e.Temporary = true)
                            .InMemoryRepository();

                        x.AddDelayedMessageScheduler();

                    });

                    services.AddTransient<RestaurantBooking>();

                    services.AddTransient<RestaurantBookingSaga>();

                    services.AddTransient<Restaurant>();

                    services.AddTransient<Answer>();

                    services.AddHostedService<Worker>();
                });
    }
}
