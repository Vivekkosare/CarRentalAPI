using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Shared.Entities
{
    public record CarCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Category { get; set; }
    }
}
