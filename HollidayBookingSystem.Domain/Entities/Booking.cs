namespace HollidayBookingSystem.Domain.Entities
{
    public abstract class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public abstract string Type { get; }
    }
    public class ApartmentBooking : Booking
    {
        public override string Type => "Apartment";
    }
    public class VehicleBooking : Booking
    {
        public override string Type => "Vehicle";
    }
    public class ShowBooking : Booking
    {
        public override string Type => "Show";
    }
}
