using CarRentalAPI.Features.Booking.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Features.Booking.AggregateRoots
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string RegistrationNumber { get; set; }
        public string Status { get; set; }
        public CarCategory Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
