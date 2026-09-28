using HollidayBookingSystem.Application.Commands.Interfaces;
using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;

namespace HollidayBookingSystem.Application.Commands
{
    public class DeleteBookingCommand : ICommand
    {
        public Booking Booking { get; }
        private readonly IBookingRepository _repo;
        private readonly Guid _bookingId;
        public DeleteBookingCommand(IBookingRepository repo, Booking booking)
        {
            _repo = repo;
            _bookingId = booking.Id;
             Booking = booking;       
        }
        public async Task ExecuteAsync()
        {
            await _repo.DeleteBookingAsync(_bookingId);
        }
    }
}
