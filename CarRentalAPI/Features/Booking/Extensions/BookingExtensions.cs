using CarRentalAPI.Features.Booking.Entities;
using System.Runtime.CompilerServices;

namespace CarRentalAPI.Features.Booking.Extensions
{
    public static class BookingExtensions
    {
        public static CarBooking MapBooking(this CarBooking existingBooking, CarBooking updatedBooking)
        {
            existingBooking.CarId = updatedBooking.CarId;
            existingBooking.Car = updatedBooking.Car;
            existingBooking.BookingDate = updatedBooking.BookingDate;
            return existingBooking;
        }
    }
}
