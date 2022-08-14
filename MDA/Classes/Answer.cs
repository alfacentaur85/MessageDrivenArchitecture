using MDA.Restaurant.Booking.Enums;

namespace MDA.Restaurant.Booking.Classes
{
    /// <summary>
    /// Ответы в меню
    /// </summary>
    public class Answer
    {
        public Mode Mode { get; set; }

        public int IdTable { get; set; }

        public uint CountOfPersons { get; set; }
    }
}
