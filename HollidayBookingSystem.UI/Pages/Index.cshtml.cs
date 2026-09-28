using Microsoft.AspNetCore.Mvc.RazorPages;
using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;

namespace HollidayBookingSystem.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBookingRepository _repository;

        public List<Booking> Bookings { get; set; } = new();

        public IndexModel(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task OnGetAsync()
        {
            Bookings = (await _repository.GetAllBookingsAsync()).ToList();
        }
    }
}
