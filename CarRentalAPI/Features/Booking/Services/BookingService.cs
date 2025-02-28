using CarRentalAPI.Data;
using CarRentalAPI.Features.Booking.Entities;
using CarRentalAPI.Features.Booking.Extensions;
using CarRentalAPI.Features.Booking.ValueObjects;
using CarRentalAPI.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Result<CarBooking>> CreateBookingAsync(BookingRequest request)
        {
            try
            {
                CarBooking booking = new CarBooking
                {
                    CarId = request.CarId,
                    CustomerId = request.CustomerId,
                    BookingDate = request.BookingDate,
                };

                var car = await _dbContext.Cars.FindAsync(booking.CarId);
                if (car is null)
                {
                    return ReturnError<CarBooking>("Car does not exist", HttpStatusCode.NotFound);
                }
                if (car?.Status != BookingStatus.Available.ToString())
                {
                    return ReturnError<CarBooking>("Car is not available for booking", HttpStatusCode.BadRequest);
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

                car.Status = BookingStatus.Booked.ToString();
                _dbContext.Cars.Update(car);

                await SaveChangesAsync();

                _logger.LogInformation($"Booking created successfully with bookingId: {bookingCreated.Entity.BookingId}");
                return new Result<CarBooking>
                {
                    Value = bookingCreated.Entity,
                    StatusCode = HttpStatusCode.Created
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

        public async Task<bool> DeleteBookingAsync(Guid Id)
        {
            var booking = await GetBookingAsync(Id);
            if (booking.StatusCode == HttpStatusCode.OK)
            {
                _dbContext.Remove(booking);
                await SaveChangesAsync();
                return true;
            }
            return false;
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
