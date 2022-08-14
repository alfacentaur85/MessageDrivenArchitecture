using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using MDA.Restaurant.Booking.Enums;
using System.Diagnostics;
using MDA.Restaurant.Messages.Classes;

namespace MDA.Restaurant.Booking.Classes
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;
        private readonly Restaurant _restaurant;
        private readonly Answer _answer;

        public Worker(IBus bus, Restaurant restaurant, Answer answer)
        {
            _bus = bus;
            _restaurant = restaurant;
            _answer = answer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {                
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (!stoppingToken.IsCancellationRequested)
            {

                Communication.MainMenu();

                if (!Communication.ChoiceMode(_answer))
                {
                    continue;
                };

                var stopWatch = new Stopwatch();

                stopWatch.Start(); //Замерим потраченное время на бронирование/сняти брони

                switch (_answer.Mode)
                {
                    case Mode.BookAsync:
                    case Mode.BookSync:
                        Communication.SetCountOfPersons(_answer);
                        break;

                    case Mode.UnBookAsync:
                    case Mode.UnBookSync:
                        Communication.SetIdTable(_answer);
                        break;
                }

                switch (_answer.Mode)
                {
                    case Mode.BookAsync:
                        var result = await _restaurant.BookFreeTableAsync(_answer); //Забронируем с уведомлением по СМС
                        await _bus.Publish(new TableBooked(NewId.NextGuid(), NewId.NextGuid(), result ?? false),
                            context => context.Durable = false, stoppingToken);
                        break;
                    case Mode.BookSync:
                        _restaurant.BookFreeTable(_answer);//Забронируем с уведомлением по звонку
                        break;
                    case Mode.UnBookAsync:
                        _restaurant.UnBookTableAsync(_answer);//Снимем бронь с уведомлением по СМС
                        break;
                    case Mode.UnBookSync: //Снимем бронь по звонку
                        _restaurant.UnBookTable(_answer);
                        break;
                }


                Console.WriteLine("Спасибо за Ваше обращение");

                stopWatch.Stop();

                var ts = stopWatch.Elapsed;

                Console.WriteLine($"{ts.Seconds}:{ts.Milliseconds}");
            }
        }
    }
}