using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MDA.Enums;
using System.Threading;
using System.Threading.Tasks;
using MDA.Classes;

namespace MDA.Classes
{
    /// <summary>
    /// Ресторан
    /// </summary>
    public class Restaurant
    {
        private readonly ushort _countOfTables = 10;

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

        public void BookFreeTableAsync(Answer answer) //асинхронное бронирование
        {
            Communication.HelloBookAsync();


            var table = _tables.FirstOrDefault(t => t.SeatsCount >= answer.CountOfPersons
                                                 && t.State == State.Free);

            table?.SetState(State.Booked);

            Communication.ResultBookAsync(table);


        }

        public void UnBookTableAsync(Answer answer) //асинхронное снятие брони
        {
            Communication.HelloUnBookAsync();

            var table = _tables.FirstOrDefault(t => t.Id == answer.IdTable
                                                 && t.State == State.Booked);

            table?.SetState(State.Free);

            Communication.ResultUnBookAsync(table);

        }
    }
}
