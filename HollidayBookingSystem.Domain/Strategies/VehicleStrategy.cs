using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Strategies.Interfaces;

namespace HollidayBookingSystem.Domain.Strategies
{
    public class VehicleStrategy : IBookingStrategy
    {
        public async Task<decimal> CalculatePriceAsync(Booking booking)
        {
            return await Task.FromResult(500m);
        }
    }
}
