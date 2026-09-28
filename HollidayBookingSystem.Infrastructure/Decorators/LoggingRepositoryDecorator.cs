using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;

namespace HollidayBookingSystem.Infrastructure.Decorators
{
    public class LoggingRepositoryDecorator : IBookingRepository
    {
        private readonly IBookingRepository _repository;

        public LoggingRepositoryDecorator(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            Console.WriteLine($"Adding booking: {booking.Id}");
            await _repository.AddBookingAsync(booking);
        }

        public async Task DeleteBookingAsync(Guid id)
        {
            Console.WriteLine($"[LOG] Deleting booking {id}");
            await _repository.DeleteBookingAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            Console.WriteLine($"[LOG] Retrieving all bookings");
            return await _repository.GetAllBookingsAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            Console.WriteLine($"[LOG] Retrieving booking {id}");
            return await _repository.GetBookingByIdAsync(id);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            Console.WriteLine($"Updating booking: {booking.Id}");
            await _repository.UpdateBookingAsync(booking);
        }
    }
}
