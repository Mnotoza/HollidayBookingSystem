using HollidayBookingSystem.Domain.Entities;

namespace HollidayBookingSystem.Domain.Factories
{
    public static class BookingFactory
    {
        public static Booking Create(string type, string customer, DateTime date)
        {
            return type switch
            {
                "Apartment" => new ApartmentBooking { CustomerName = customer, BookingDate = date },
                "Vehicle" => new VehicleBooking { CustomerName = customer, BookingDate = date },
                "Show" => new ShowBooking { CustomerName = customer, BookingDate = date },
                _ => throw new ArgumentException("Invalid booking type")
            };
        }
    }
}
