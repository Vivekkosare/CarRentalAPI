namespace CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects
{
    public class BookingWithRegistration
    {
        public Guid? BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
        public DateTime BookingDate { get; set; }
        public Guid? RegistrationId { get; set; }
        public string CustomerSSN { get; set; }
        public DateTime? PickUp { get; set; }
        public int? PickUpMeterReading { get; set; }
        public DateTime? Return { get; set; }
        public int? ReturnMeterReading { get; set; }
    }
}
