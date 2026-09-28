using HollidayBookingSystem.Domain.Entities;

namespace HollidayBookingSystem.Domain.Strategies.Interfaces
{
    public interface IBookingStrategy
    {
        Task<decimal> CalculatePriceAsync(Booking booking);
    }
}
