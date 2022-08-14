namespace MDA.Restaurant.Booking.Enums
{
    public enum Mode
    {
        /// <summary>
        /// Бронирование с уведомлением (асинхронно)
        /// </summary>
        BookAsync = 1,

        /// <summary>
        /// Бронирование по звонку (синхронно)
        /// </summary>
        BookSync = 2,

        /// <summary>
        /// Снятие брони с уведомлением (асинхронно)
        /// </summary>
        UnBookAsync = 3,

        /// <summary>
        /// Снятие брони по звонку (синхронно)
        /// </summary>
        UnBookSync = 4
       
    }
}
