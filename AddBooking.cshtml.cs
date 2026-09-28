using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HollidayBookingSystem.UI.Models; // Adjust if your Booking model is in a different namespace

namespace HollidayBookingSystem.UI.Pages
{
    public class AddBookingModel : PageModel
    {
        [BindProperty]
        public Booking Booking { get; set; }

        public void OnGet()
        {
            Booking = new Booking();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Add logic to save the booking

            return RedirectToPage("Index");
        }
    }
}                                                           