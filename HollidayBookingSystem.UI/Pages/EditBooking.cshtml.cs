using HollidayBookingSystem.Application.Commands;
using HollidayBookingSystem.Application.Handlers;
using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;
using HollidayBookingSystem.UI.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HollidayBookingSystem.UI.Pages
{
    public class EditBookingModel : PageModel
    {
        private readonly IBookingRepository _repository;
        private readonly BookingCommandHandler _commandHandler;

        [BindProperty]
        public BookingInput Input { get; set; } = new();

        public EditBookingModel(IBookingRepository repository, BookingCommandHandler commandHandler)
        {
            _repository = repository;
            _commandHandler = commandHandler;
        }

        public IActionResult OnGet(Guid id)
        {
            var booking = _repository.GetBookingByIdAsync(id).Result;
            if (booking == null)
                return NotFound();

            Input.CustomerName = booking.CustomerName;
            Input.Type = booking.Type;
            Input.BookingDate = booking.BookingDate;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return Page();

            var booking = await _repository.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            booking.CustomerName = Input.CustomerName;
            booking.BookingDate = Input.BookingDate;

            var command = new EditBookingCommand(_repository, booking);
            await _commandHandler.HandleAsync(command);
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var booking = await _repository.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            var command = new DeleteBookingCommand(_repository, booking);
            await _commandHandler.HandleAsync(command);

            // Redirect back to list after deletion
            return RedirectToPage("/Index");
        }


    }
}
