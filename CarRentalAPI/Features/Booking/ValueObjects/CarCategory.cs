using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Features.Booking.ValueObjects
{
    public record CarCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Category { get; set; }
    }
}
