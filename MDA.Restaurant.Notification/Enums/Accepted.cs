using System;

namespace MDA.Restaurant.Notification.Enums
{
    /// <summary>
    /// Принято
    /// </summary>
    [Flags]
    public enum Accepted
    {
        Rejected = 0,
        Kitchen = 1,
        Booking = 2,
        All = Kitchen | Booking
    }
}