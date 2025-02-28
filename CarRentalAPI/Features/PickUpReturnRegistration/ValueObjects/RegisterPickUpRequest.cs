namespace CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects
{
    public record PickUpReturnRequest
    {
        public Guid BookingId { get; set; }
        public string CustomerSSN { get; set; }
        public int ReturnMeterReading { get; set; }
    }
}
