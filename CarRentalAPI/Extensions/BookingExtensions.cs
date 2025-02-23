using CarRentalAPI.Features.Booking.AggregateRoots;
using System.Runtime.CompilerServices;

namespace CarRentalAPI.Extensions
{
    public static class BookingExtensions
    {
        public static CarBooking MapBooking(this CarBooking existingBooking, CarBooking updatedBooking)
        {
            existingBooking.CarId = updatedBooking.CarId;
            existingBooking.Car = updatedBooking.Car;
            existingBooking.BookingDate = updatedBooking.BookingDate;
            existingBooking.PickUpDate = updatedBooking.PickUpDate ?? updatedBooking.PickUpDate;
            existingBooking.ReturnDate = updatedBooking.ReturnDate ?? updatedBooking.ReturnDate;
            return existingBooking;
        }
    }
}
