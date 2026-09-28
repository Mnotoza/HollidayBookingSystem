using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Events;

namespace HollidayBookingSystem.Infrastructure.Events
{
    public class EmailNotifier : IBookingObserver
    {
        public async Task OnBookingChangedAsync(string action, Booking booking)
        {
            // Implement email notification logic here
            await Task.Delay(500);
            Console.WriteLine($"[Email] {action} booking {booking.Id}");
        }
    }
}
