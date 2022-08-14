using System.Collections.Generic;
using System.Text;
using System.Linq;
using MDA.Restaurant.Booking.Enums;
using MDA.Producer;
using System.Threading.Tasks;

namespace MDA.Restaurant.Booking.Classes
{
    /// <summary>
    /// Ресторан
    /// </summary>
    public class Restaurant
    {
        private readonly ushort _countOfTables = 10;

        private readonly RabbitProducer _producer = new RabbitProducer();

        private readonly List<Table> _tables = new ();

        public Restaurant()
        {
            for (ushort i = 1; i <= _countOfTables; i++)
            {
                _tables.Add(new Table(i));
            }
        }

        public void BookFreeTable(Answer answer) //синхронное бронирование
        {
            Communication.HelloBookSync();

            var table = _tables.FirstOrDefault(t => t.SeatsCount >= answer.CountOfPersons
                                                 && t.State == State.Free);

            table?.SetState(State.Booked);

            

            Communication.ResultBookSync(table);
        }

        public void UnBookTable(Answer answer) //синхронное снятие бронирование
        {
            Communication.HelloUnBookSync();

            var table = _tables.FirstOrDefault(t => t.Id == answer.IdTable
                                                 && t.State == State.Booked);

            table?.SetState(State.Free);

            Communication.ResultUnBookSync(table);
        }

        public async Task<bool?> BookFreeTableAsync(Answer answer) //асинхронное бронирование
        {
            Communication.HelloBookAsync();


            var table = _tables.FirstOrDefault(t => t.SeatsCount >= answer.CountOfPersons
                                                 && t.State == State.Free);

            await Task.Delay(1000 * 5); //у нас нерасторопные менеджеры, 5 секунд они находятся в поисках стола

            return table?.SetState(State.Booked);

        }

        public void UnBookTableAsync(Answer answer) //асинхронное снятие брони
        {
            Communication.HelloUnBookAsync();

            var table = _tables.FirstOrDefault(t => t.Id == answer.IdTable
                                                 && t.State == State.Booked);

            table?.SetState(State.Free);

            _producer.SendToQueue(Encoding.UTF8.GetBytes(Communication.ResultUnBookAsync(table)), "Booking");

        }
    }
}
