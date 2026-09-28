using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Events;

namespace HollidayBookingSystem.Infrastructure.Events
{
    public class AuditLogger : IBookingObserver
    {
        public async Task OnBookingChangedAsync(string action, Booking booking)
        {
            // Implement audit logging logic here
            await Task.Delay(500);
            Console.WriteLine($"[Audit] {action} booking {booking.Id}");
        }
    }
}
