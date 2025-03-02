namespace CarRentalAPI.Shared.Entities
{
    public class BookingHistory
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public string Status { get; set; }
        public DateTime StatusChangedAt { get; set; }
        public int? MeterReading { get; set; }
        public bool RentalPriceCalculated { get; set; } = false;

    }
}
