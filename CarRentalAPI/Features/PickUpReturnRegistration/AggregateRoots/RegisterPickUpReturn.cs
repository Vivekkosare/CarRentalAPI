using CarRentalAPI.Features.Booking.AggregateRoots;
using CarRentalAPI.Features.Booking.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Features.PickUpReturnRegistration.AggregateRoots
{
    public class RegisterPickUpReturn
    {
        [Key]
        public Guid RegistrationId { get; set; }
        public Guid BookingId { get; set; }
        public string CustomerSSN { get; set; }
        public DateTime PickUp { get; set; }
        public int PickUpMeterReading { get; set; }
        public DateTime? Return { get; set; }
        public int? ReturnMeterReading { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CarBooking Booking { get; set; }
    }
}
