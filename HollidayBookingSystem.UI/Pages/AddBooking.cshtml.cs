using HollidayBookingSystem.Application.Handlers;
using HollidayBookingSystem.Application.Commands;
using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;
using HollidayBookingSystem.UI.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HollidayBookingSystem.UI.Pages
{
    public class AddBookingModel : PageModel
    {
        private readonly BookingCommandHandler _commandHandler;
        private readonly IBookingRepository _bookingRepository;

        [BindProperty]
        public BookingInput Input { get; set; } = new();

        public AddBookingModel(BookingCommandHandler commandHandler, IBookingRepository bookingRepository)
        {
            _commandHandler = commandHandler;
            _bookingRepository = bookingRepository;
        }

        public void OnGet()
        {
            // Display empty form
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Booking booking = Input.Type switch
            {
                "Apartment" => new ApartmentBooking(),
                "Vehicle" => new VehicleBooking(),
                "Show" => new ShowBooking(),
                _ => throw new InvalidOperationException("Unknown booking type")
            };

            booking.CustomerName = Input.CustomerName;
            booking.BookingDate = Input.BookingDate;

            var command = new AddBookingCommand(_bookingRepository, booking);
            await _commandHandler.HandleAsync(command);
            return RedirectToPage("/Index");
        }

    }
}
