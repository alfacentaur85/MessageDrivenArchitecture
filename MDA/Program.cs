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
                var answer = new Answer();

                Communication.MainMenu();

                if (!Communication.ChoiceMode(answer))
                {
                    continue;
                };

                var stopWatch = new Stopwatch();

                stopWatch.Start(); //Замерим потраченное время на бронирование/сняти брони

                switch (answer.Mode)
                {
                    case Mode.BookAsync:      
                    case Mode.BookSync:
                        Communication.SetCountOfPersons(answer);    
                        break;

                    case Mode.UnBookAsync: 
                    case Mode.UnBookSync:
                        Communication.SetIdTable(answer);
                        break;
                }

                switch (answer.Mode)
                {
                    case Mode.BookAsync:
                        rest.BookFreeTableAsync(answer); //Забронируем с уведомлением по СМС
                        break;
                    case Mode.BookSync:
                        rest.BookFreeTable(answer);//Забронируем с уведомлением по звонку
                        break;
                    case Mode.UnBookAsync:
                        rest.UnBookTableAsync(answer);//Снимем бронь с уведомлением по СМС
                        break;
                    case Mode.UnBookSync: //Снимем бронь по звонку
                        rest.UnBookTable(answer);
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
