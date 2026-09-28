namespace HollidayBookingSystem.UI.Pages.Models
{
    public class BookingInput
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string Type { get; set; } = string.Empty;
    }
}
