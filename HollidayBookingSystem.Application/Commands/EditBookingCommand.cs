using HollidayBookingSystem.Application.Commands.Interfaces;
using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;

namespace HollidayBookingSystem.Application.Commands
{
    public class EditBookingCommand : ICommand
    {
        public Booking Booking { get; }
        private readonly IBookingRepository _repo;
        public EditBookingCommand(IBookingRepository repo, Booking booking)
        {
            _repo = repo;
            Booking = booking;
        }
        public async Task ExecuteAsync()
        {
            await _repo.UpdateBookingAsync(Booking);
        }
    }
}
