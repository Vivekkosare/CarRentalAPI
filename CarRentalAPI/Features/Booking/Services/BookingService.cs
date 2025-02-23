using CarRentalAPI.Data;
using CarRentalAPI.Extensions;
using CarRentalAPI.Features.Booking.AggregateRoots;
using CarRentalAPI.Features.Booking.ValueObjects;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarRentalAPI.Features.Booking.Services
{
    public class BookingService : IBookingService
    {
        private CarDbContext _dbContext;
        private readonly ILogger<BookingService> _logger;

        public BookingService(CarDbContext dbContext, ILogger<BookingService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Result<CarBooking>> CreateBookingAsync(CarBooking booking)
        {
            try
            {

                var car = await _dbContext.Cars.FindAsync(booking.CarId);
                if (car is null)
                {
                    return ReturnError<CarBooking>("Car does not exist", HttpStatusCode.NotFound);
                }
                var customer = await _dbContext.Customers.FindAsync(booking.CustomerId);
                if (customer is null)
                {
                    return ReturnError<CarBooking>("Customer not found", HttpStatusCode.NotFound);
                }
                var existingBooking = await _dbContext.Bookings.FindAsync(booking.BookingId);
                if (existingBooking is not null)
                {
                    return ReturnError<CarBooking>("Booking already exists", HttpStatusCode.BadRequest);
                }
                var bookingCreated = await _dbContext.Bookings.AddAsync(booking);
                await SaveChangesAsync();

                _logger.LogInformation($"Booking created successfully with bookingId: {bookingCreated.Entity.BookingId}");
                return new Result<CarBooking>
                {
                    Value = bookingCreated.Entity
                };
            }
            catch (Exception ex)
            {
                return ReturnError<CarBooking>($"Something happened: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        private Result<T> ReturnError<T>(string error, HttpStatusCode statusCode) where T : class
        {
            _logger.LogError(error);
            return new Result<T>
            {
                Error = error,
                StatusCode = statusCode
            };
        }

        public async Task DeleteBookingAsync(Guid Id)
        {
            var booking = await GetBookingAsync(Id);
            _dbContext.Remove(booking);
            await SaveChangesAsync();
        }

        public async Task<Result<CarBooking>> GetBookingAsync(Guid id)
        {
            var booking = await _dbContext.Bookings.FindAsync(id);
            if (booking is null)
            {
                return ReturnError<CarBooking>("Booking not found", HttpStatusCode.NotFound);
            }
            return new Result<CarBooking>
            {
                Value = booking
            };
        }

        public async Task<Result<CarBooking>> UpdateBookingAsync(Guid id, CarBooking booking)
        {
            var existingBooking = await GetBookingAsync(id);
            existingBooking.Value.MapBooking(booking);

            _dbContext.Update(existingBooking);
            await SaveChangesAsync();
            _logger.LogInformation($"Booking updated with Id: {id}");
            return existingBooking;
        }

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
