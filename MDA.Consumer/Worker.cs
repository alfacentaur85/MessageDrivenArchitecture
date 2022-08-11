﻿using System.Text;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace MDA.Consumer
{
    public class Worker: BackgroundService
    {
        private readonly Consumer _consumerBooking;

        private readonly Consumer _consumerUnBooking;

        public Worker()
        {
            _consumerBooking = new Consumer("Booking");
            _consumerUnBooking = new Consumer("UnBooking");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => 
            {
                _consumerBooking.Receive((sender, args) =>
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received {message}");
                });

                _consumerUnBooking.Receive((sender, args) =>
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received {message}");
                });
            });
        }
    }
}
