using System;
using MassTransit;
using MassTransit.Audit;
using Microsoft.Extensions.DependencyInjection;
using MDA.Restaurant.Booking.Classes.Consumers;
using MDA.Restaurant.Messages.Interfaces;
using MDA.Restaurant.Messages.Classes;
using System.Security.Authentication;
using Microsoft.AspNetCore.Builder;
using Prometheus;

namespace MDA.Restaurant.Booking.Classes 
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMassTransit(x =>
            {
                services.AddSingleton<IMessageAuditStore, AuditStore>();

                var serviceProvider = services.BuildServiceProvider();
                var auditStore = serviceProvider.GetService<IMessageAuditStore>();

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

                    cfg.UseInMemoryOutbox();

                    cfg.UseDelayedMessageScheduler();

                    cfg.UsePrometheusMetrics(serviceName: "booking_service");

                    cfg.ConfigureEndpoints(context);

                    cfg.ConnectSendAuditObservers(auditStore);

                    cfg.ConnectConsumeAuditObserver(auditStore);

                }));


                x.AddConsumer<RestaurantBookingRequestConsumer>();
                //   x.AddConsumer<BookingRequestFaultConsumer>();

                x.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                    .InMemoryRepository();

                x.AddDelayedMessageScheduler();


            });

            services.AddMassTransit(x =>
            {
                services.AddSingleton<IMessageAuditStore, AuditStore>();

                var serviceProvider = services.BuildServiceProvider();
                var auditStore = serviceProvider.GetService<IMessageAuditStore>();

                x.AddConsumer<RestaurantBookingRequestConsumer>();
                //   x.AddConsumer<BookingRequestFaultConsumer>();

                x.AddSagaStateMachine<RestaurantBookingSaga, RestaurantBooking>()
                    .InMemoryRepository();

                x.AddDelayedMessageScheduler();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UsePrometheusMetrics(serviceName: "booking_service");
                    cfg.UseDelayedMessageScheduler();
                    cfg.UseInMemoryOutbox();
                    cfg.ConfigureEndpoints(context);

                    cfg.ConnectSendAuditObservers(auditStore);
                    cfg.ConnectConsumeAuditObserver(auditStore);
                });
            });

            services.Configure<MassTransitHostOptions>(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(30);
                options.StopTimeout = TimeSpan.FromMinutes(1);
            });

            services.AddTransient<RestaurantBooking>();
            services.AddTransient<RestaurantBookingSaga>();
            services.AddTransient<Restaurant>();
            services.AddTransient<Answer>();
            services.AddSingleton<IInMemoryRepository<IBookingRequest>, InMemoryRepository<IBookingRequest>>();

            services.AddHostedService<Worker>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
                endpoints.MapControllers();
            });
        }
    }
}

