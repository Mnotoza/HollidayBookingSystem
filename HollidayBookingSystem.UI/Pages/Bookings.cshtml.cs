using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HollidayBookingSystem.UI.Pages
{
    public class BookingsModel : PageModel
    {
        private readonly IBookingRepository _repository;

        public List<Booking> Bookings { get; set; } = new();

        public BookingsModel(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task OnGetAsync()
        {
            Bookings = (await _repository.GetAllBookingsAsync()).ToList();
        }
    }
}
