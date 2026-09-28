using HollidayBookingSystem.Domain.Entities;

namespace HollidayBookingSystem.Domain.Repositories
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(Booking booking);
        Task<Booking?> GetBookingByIdAsync(Guid id);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Guid id);
    }
}
