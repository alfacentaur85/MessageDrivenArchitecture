using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDA.Enums;
using System.Threading;

namespace MDA.Classes
{
    /// <summary>
    /// Коммуникации с клиентом
    /// </summary>
    public static class Communication
    {
        private static readonly ushort _multiplierDelay = 5;

        public static void MainMenu()
        {
            StringBuilder sb = new();

            sb.Append($"Привет! Желаете забронировать столик?");
            sb.Append($"\n{(int)Mode.BookAsync} - мы уведомим вас по СМС (асинхронно)");
            sb.Append($"\n{(int)Mode.BookSync} - подождите на линии, мы оповестим (синхронно)");
            sb.Append($"\n{(int)Mode.UnBookAsync} - Снять бронь с уведомлением по СМС (асинхронно)");
            sb.Append($"\n{(int)Mode.UnBookSync} - Снять бронь по звонку (синхронно)");
            Console.WriteLine(sb.ToString());
        }

        public static bool ChoiceMode(Answer answer)
        {
            if (int.TryParse(Console.ReadLine(), out var choice))
            {
                foreach(var e in Enum.GetValues(typeof(Mode)).Cast<Mode>())
                {
                    if ((int)e == choice)
                    {
                        answer.Mode = (Mode)choice;
                        return true;
                    }
                }
            }
            Console.WriteLine("Пожалуйста, введите номер одного из пунктов меню");
            return false;
        }

        public static bool SetIdTable(Answer answer)
        {
            while (true)
            {
                Console.WriteLine("Пожалуйста, введите номер столика - целое число");
                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    answer.IdTable = choice;
                    break;
                }
                continue;           
            }
            return true;
        }

        public static bool SetCountOfPersons(Answer answer)
        {
            while (true)
            {
                Console.WriteLine("Пожалуйста, введите количество гостей - целое положительное число");
                if (uint.TryParse(Console.ReadLine(), out var choice))
                {
                    answer.CountOfPersons = choice;
                    break;
                }
                continue;
            }
            return true;
        }

        public static void HelloBookSync()
        {
            Console.WriteLine("Добрый день! Подождите секунду я  подберу столик и подтвержу вашу бронь, оставайтесь на линии");
        }

        public static void ResultBookSync(Table table)
        {
            Thread.Sleep(1000 * _multiplierDelay); //У нас нерасторопные менеджеры. 5 секунд они находятся в поиске стола

            Console.WriteLine(table is null
               ? "К сожалению, сейчас все столики заняты"
               : $"Готово. Ваш столик номер {table.Id}");
        }

        public static void HelloBookAsync()
        {
            Task.Run(async () =>
            {
                await Task.Delay(0);

                Console.WriteLine("Добрый день! Подождите секунду я  подберу столик и подтвержу вашу бронь. Вам придет уведомление");
            });
        }

        public static string ResultBookAsync(Table table)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000 * _multiplierDelay); //У нас нерасторопные менеджеры. 5 секунд они находятся в поиске стола
              
            });

            return table is null
                ? "УВЕДОМЛЕНИЕ: К сожалению, сейчас все столики заняты"
                : $"УВЕДОМЛЕНИЕ: Готово. Ваш столик номер {table.Id}";
        }

        public static void HelloUnBookSync()
        {
            Console.WriteLine("Добрый день! Подождите секунду я сниму вашу бронь, оставайтесь на линии");
        }

        public static void ResultUnBookSync(Table table)
        {
            Thread.Sleep(1000 * _multiplierDelay); //У нас нерасторопные менеджеры. 5 секунд они снимают бронь

            Console.WriteLine(table is null
                ? $"Столик {table.Id} не был забронирован"
                : $"Готово. Бронь со столика номер {table.Id} снята");
        }

        public static void HelloUnBookAsync()
        {
            Task.Run(async () =>
            {
                await Task.Delay(0);

                Console.WriteLine("Добрый день! Подождите секунду я сниму вашу бронь. Вам придет уведомление");
            });        
        }

        public static string ResultUnBookAsync(Table table)
        {
            Task.Run(async () =>
            {

                await Task.Delay(1000 * _multiplierDelay); //У нас нерасторопные менеджеры. 5 секунд они находятся в поиске стола

            });
            return table is null
                ? $"УВЕДОМЛЕНИЕ: Столик {table.Id} не был забронирован"
                : $"УВЕДОМЛЕНИЕ: Готово. Бронь со столика номер {table.Id} снята";

        }

    }
}
