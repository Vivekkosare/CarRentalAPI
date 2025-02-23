using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Features.Booking.AggregateRoots
{
    public class CarBooking
    {
        [Key]
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Car Car { get; set; }
        public Customer Customer { get; set; }
    }
}
