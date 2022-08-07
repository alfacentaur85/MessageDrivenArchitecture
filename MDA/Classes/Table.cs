using System;
using System.Collections.Generic;
using System.Text;
using MDA.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace MDA.Classes
{
    public class Table
    {
        private readonly int _minSeats = 2;

        private readonly int _maxSeats = 6;

        private readonly int periodTimer = 20000;

        private readonly int periodInfinite = 20000;

        private readonly Timer _timer;

        public State State { get; private set; }

        public int SeatsCount { get; }

        public int Id { get; }

        public Table(int id)
        {
            Id = id; //В учебом примере просто присвоим Ид при вызове
            State = State.Free; //Новый стол всегда свободен
            SeatsCount = new Random().Next(_minSeats, _maxSeats); //Случайной количество мест за столом: от 2 до 5
           
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(UnBookTableAutoAsync);
            // создаем таймер пока не выполняющийся
            _timer = new Timer(tm, null, 0, periodInfinite);
                      
        }
        
        public bool SetState(State state)
        {
            if (state == State)
                return false;

            State = state;

            switch (State)
            {
                case State.Free:
                    _timer.Change(0, periodInfinite);
                    break;

                case State.Booked:
                    _timer.Change(periodTimer, periodTimer); //запускаем таймер первый раз через periodTimer мс с периодом periodTimer
                    break;
            }

            return true;
        }

        private async void UnBookTableAutoAsync(object obj)
        {
            await Task.Run(() => { 
                if(this.State == State.Booked)
                {
                    this.State = State.Free;
                    Console.WriteLine($"Автомат: со столика номер {this.Id} была снята бронь."); 
                }
            });
        }


    }
}
