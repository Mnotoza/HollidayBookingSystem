using HollidayBookingSystem.Domain.Entities;

namespace HollidayBookingSystem.Domain.Events
{
    public interface IBookingObserver
    {
       Task OnBookingChangedAsync(string action, Booking booking);
    }
}
