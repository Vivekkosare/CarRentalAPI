namespace CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects
{
    public record RentalRatesInput
    {
        public  Guid BookingId { get; set; }
        public decimal BaseDayRental { get; set; }
        public decimal BaseKmPrice { get; set; }
    }
}
