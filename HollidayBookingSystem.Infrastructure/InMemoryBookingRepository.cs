using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;

namespace HollidayBookingSystem.Infrastructure;

public class InMemoryBookingRepository : IBookingRepository
{
    private readonly Dictionary<Guid, Booking> _store = new();
    public async Task AddBookingAsync(Booking booking)
    {
        _store[booking.Id] = booking;

        Console.WriteLine($"[Repository] Added booking for {booking.CustomerName} ({booking.Type}) at {booking.BookingDate}");
        Console.WriteLine($"[Repository] Total bookings in memory: {_store.Count}");

        await Task.CompletedTask;
    }
    public async Task DeleteBookingAsync(Guid id)
    {
        _store.Remove(id);
        Console.WriteLine($"[Repository] Deleted booking {id}");
        await Task.CompletedTask;
    }
    public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        Console.WriteLine($"[Repository] Returning {_store.Count} bookings");
        return await Task.FromResult<IEnumerable<Booking>>(_store.Values);
    }
    public async Task<Booking?> GetBookingByIdAsync(Guid id)
    {
        _ = _store.TryGetValue(id, out Booking? b) ? b : null;
        return await Task.FromResult(b);    
    }
    public async Task UpdateBookingAsync(Booking booking)
    {
        _store[booking.Id] = booking;
        await Task.CompletedTask;
    }
}
