using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MDA.Restaurant.Kitchen.Classes.Consumers;
using MDA.Restaurant.Kitchen.Classes;
using System.Security.Authentication;

namespace MDA.Restaurant.Kitchen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<KitchenTableBookedConsumer>();

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

                    });
                   
                    services.AddSingleton<Manager>();

                    services.AddMassTransitHostedService(true);
                });
    }
}
