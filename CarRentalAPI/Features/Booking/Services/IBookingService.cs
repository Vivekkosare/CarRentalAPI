using CarRentalAPI.Features.Booking.AggregateRoots;
using CarRentalAPI.Features.Booking.ValueObjects;

namespace CarRentalAPI.Features.Booking.Services
{
    public interface IBookingService
    {
        Task<Result<CarBooking>> CreateBookingAsync(CarBooking booking);
        Task<Result<CarBooking>> GetBookingAsync(Guid id);
        Task<Result<CarBooking>> UpdateBookingAsync(Guid id, CarBooking booking);
        Task DeleteBookingAsync(Guid Id);


    }
}
