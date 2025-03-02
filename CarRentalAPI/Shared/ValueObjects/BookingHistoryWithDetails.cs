using CarRentalAPI.Shared.Entities;

namespace CarRentalAPI.Shared.ValueObjects
{
    public record BookingHistoryWithDetails
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public string CarName { get; set; }
        public string RegistrationNumber { get; set; }
        public string CarCategory { get; set; }
        public List<BookingHistory> BookingHistories { get; set; }
        public Customer Customer { get; set; }
    }
}
