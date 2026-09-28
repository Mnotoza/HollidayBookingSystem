using HollidayBookingSystem.Application.Commands;
using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Events;
using HollidayBookingSystem.Domain.Repositories;
using HollidayBookingSystem.Domain.Strategies.Interfaces;

namespace HollidayBookingSystem.Application.Handlers
{
    public class BookingCommandHandler
    {
        private readonly IBookingRepository _repository;
        private readonly IEnumerable<IBookingObserver> _observers;
        private readonly IDictionary<string, IBookingStrategy> _strategies;
        public BookingCommandHandler(
                IBookingRepository repository,
                IEnumerable<IBookingObserver> observers,
                IDictionary<string, IBookingStrategy> strategies)
        {
            _repository = repository;
            _observers = observers;
            _strategies = strategies;
        }
        public async Task HandleAsync(AddBookingCommand command)
        {
            if (_strategies.TryGetValue(command.Booking.Type, out var strategy))
            {
                var price = await strategy.CalculatePriceAsync(command.Booking);
                Console.WriteLine($"Calculated price for {command.Booking.Type}: {price:C}");
            }

            await _repository.AddBookingAsync(command.Booking);
            await NotifyObserversAsync("Added", command.Booking);
        }
        public async Task HandleAsync(EditBookingCommand command)
        {
            var existing = (await _repository.GetBookingByIdAsync(command.Booking.Id));
            if (existing == null)
                throw new InvalidOperationException("Booking not found");

            existing.CustomerName = command.Booking.CustomerName;
            existing.BookingDate = command.Booking.BookingDate;

            await _repository.UpdateBookingAsync(existing);
            await NotifyObserversAsync("Edited", existing);
        }

        public async Task HandleAsync(DeleteBookingCommand command)
        {
            await _repository.DeleteBookingAsync(command.Booking.Id);
            await NotifyObserversAsync("Deleted", command.Booking);
        }
        private async Task NotifyObserversAsync(string action, Booking booking)
        {
            var tasks = _observers.Select(o => o.OnBookingChangedAsync(action, booking));
            await Task.WhenAll(tasks);
        }
    }
}
