using System;
using MDA.Classes;
using System.Diagnostics;
using MDA.Enums;

namespace MDA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var rest = new Restaurant();

            while (true)
            {
                var communication = new Communication();

                communication.MainMenu();

                if (!communication.ChoiceMode())
                {
                    continue;
                };

                var stopWatch = new Stopwatch();

                stopWatch.Start(); //Замерим потраченное время на бронирование/сняти брони

                switch (communication.Mode)
                {
                    case Mode.BookAsync:      
                    case Mode.BookSync:
                        communication.SetCountOfPersons();    
                        break;

                    case Mode.UnBookAsync: 
                    case Mode.UnBookSync: 
                        communication.SetIdTable();
                        break;
                }

                switch (communication.Mode)
                {
                    case Mode.BookAsync:
                        rest.BookFreeTableAsync(communication); //Забронируем с уведомлением по СМС
                        break;
                    case Mode.BookSync:
                        rest.BookFreeTable(communication);//Забронируем с уведомлением по звонку
                        break;
                    case Mode.UnBookAsync:
                        rest.UnBookTableAsync(communication);//Снимем бронь с уведомлением по СМС
                        break;
                    case Mode.UnBookSync: //Снимем бронь по звонку
                        rest.UnBookTable(communication);
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
