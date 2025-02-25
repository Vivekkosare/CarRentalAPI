using CarRentalAPI.Features.Booking.ValueObjects;
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

        //public decimal GetRentalPrice(decimal baseDayRental, decimal baseKmPrice)
        //{
        //    var numberOfDays = (ReturnDate - PickUpDate).Value.Days;
        //    //decimal numberOfKm = decimal.Parse(Car.CurrentMeterReading);
        //    return Car.Category.Category.ToLower() switch
        //    {
        //        "small" => baseDayRental * numberOfDays,
        //        "medium" => (baseDayRental * numberOfDays * (decimal)1.3) + baseKmPrice * numberOfKm,
        //        "large" => (baseDayRental * numberOfDays * (decimal)1.5) + baseKmPrice * numberOfKm,
        //        "suv" => (baseDayRental * numberOfDays * (decimal)1.5) + baseKmPrice * numberOfKm * (decimal)1.5,
        //        "minivan" => (baseDayRental * numberOfDays * (decimal)1.7) + baseKmPrice * numberOfKm * (decimal)1.7,
        //        _ => throw new ArgumentException($"Unknown category{Car.Category.Category}")
        //    };
        //}
    }
}
