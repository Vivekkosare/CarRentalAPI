using CarRentalAPI.Features.Booking.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Features.Booking.AggregateRoots
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string RegistrationNumber { get; set; }
        public BookingStatus Status { get; set; }
        public string CurrentMeterReading { get; set; }
        public CarCategory Category { get; set; }

    }
}
