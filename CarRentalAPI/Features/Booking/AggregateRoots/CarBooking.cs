using CarRentalAPI.Features.Booking.ValueObjects;
using CarRentalAPI.Features.PickUpReturnRegistration.AggregateRoots;
using CarRentalAPI.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Features.Booking.AggregateRoots
{
    public class CarBooking
    {
        [Key]
        public Guid? BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
        public DateTime BookingDate { get; set; }
        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public RegisterPickUpReturn? RegisterPickUpReturn { get; set; }

        
    }
}
