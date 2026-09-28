using HollidayBookingSystem.Application.Commands.Interfaces;
using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;

namespace HollidayBookingSystem.Application.Commands
{
    public class AddBookingCommand : ICommand
    {
        private readonly IBookingRepository _repo;
        public Booking Booking { get; }
         public AddBookingCommand(IBookingRepository repo, Booking booking)
        {
            _repo = repo;
            Booking = booking;
        }
        public async Task ExecuteAsync()
        {
            await _repo.AddBookingAsync(Booking);
        }
    }
}
