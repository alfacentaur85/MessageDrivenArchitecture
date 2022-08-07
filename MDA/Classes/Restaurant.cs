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
    public class Restaurant
    {
        private readonly uint _countOfTables = 10;

        private readonly List<Table> _tables = new List<Table>();

        public Restaurant()
        {
            for (ushort i = 1; i <= _countOfTables; i++)
            {
                _tables.Add(new Table(i));
            }
        }

        public void BookFreeTable(Communication communication) //синхронное бронирование
        {
            Communication.HelloBookSync();

            var table = _tables.FirstOrDefault(t => t.SeatsCount >= communication.CountOfPersons
                                                 && t.State == State.Free);

            table?.SetState(State.Booked);

            

            Communication.ResultBookSync(table);
        }

        public void UnBookTable(Communication communication) //синхронное снятие бронирование
        {
            Communication.HelloUnBookSync();

            var table = _tables.FirstOrDefault(t => t.Id == communication.IdTable
                                                 && t.State == State.Booked);

            table?.SetState(State.Free);

            Communication.ResultUnBookSync(table);
        }

        public void BookFreeTableAsync(Communication communication) //асинхронное бронирование
        {
            Communication.HelloBookAsync();


            var table = _tables.FirstOrDefault(t => t.SeatsCount >= communication.CountOfPersons
                                                 && t.State == State.Free);

            table?.SetState(State.Booked);

            Communication.ResultBookAsync(table);


        }

        public void UnBookTableAsync(Communication communication) //асинхронное снятие брони
        {
            Communication.HelloUnBookAsync();

            var table = _tables.FirstOrDefault(t => t.Id == communication.IdTable
                                                 && t.State == State.Booked);

            table?.SetState(State.Free);

            Communication.ResultUnBookAsync(table);

        }
    }
}
