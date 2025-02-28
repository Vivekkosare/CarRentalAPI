using CarRentalAPI.Data;
using CarRentalAPI.Features.Booking.ValueObjects;
using CarRentalAPI.Features.PickUpReturnRegistration.Entities;
using CarRentalAPI.Features.PickUpReturnRegistration.Extensions;
using CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects;
using CarRentalAPI.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CarRentalAPI.Features.PickUpReturnRegistration.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly CarDbContext _dbContext;
        private readonly ILogger<RegistrationService> _logger;

        public RegistrationService(CarDbContext dbContext, ILogger<RegistrationService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Result<RegisterPickUpReturn>> RegisterPickUp(RegisterPickUpReturn registerPickUp)
        {
            try
            {
                if (registerPickUp == null)
                    return BadRequest<RegisterPickUpReturn>("Invalid Request");

                if (string.IsNullOrWhiteSpace(registerPickUp.CustomerSSN))
                    return BadRequest<RegisterPickUpReturn>("Customer's social security number (SSN) is missing");

                //check if booking exist for the customer?
                var bookingByCustomer = await _dbContext.Bookings
                                                .Include(b => b.Car).FirstOrDefaultAsync(b => b.BookingId == registerPickUp.BookingId);
                if (bookingByCustomer is null)
                    return BadRequest<RegisterPickUpReturn>($"Can't find any booking by customer: {registerPickUp.CustomerSSN}");

                if (bookingByCustomer.Car.CurrentMeterReading < 0)
                    return BadRequest<RegisterPickUpReturn>("Current meter reading while pickup is missing or invalid");

                //check if pickup was already registered?
                var pickUp = await GetRegisteredPickUp(registerPickUp.BookingId);
                if (pickUp.StatusCode == HttpStatusCode.OK)
                {
                    return BadRequest<RegisterPickUpReturn>($"Pickup already registered for booking: {registerPickUp.BookingId}");
                }

                _logger.LogInformation("Vehicle pickup request is valid and is being registered for pickup");
                registerPickUp.PickUp = DateTime.UtcNow;
                registerPickUp.PickUpMeterReading = bookingByCustomer.Car.CurrentMeterReading;
                registerPickUp.CreatedAt = DateTime.UtcNow;

                var pickUpRegistered = await _dbContext.RegisterPickUpReturns.AddAsync(registerPickUp);
                await _dbContext.SaveChangesAsync();
                return new Result<RegisterPickUpReturn> { Value = pickUpRegistered.Entity, StatusCode = HttpStatusCode.Created };

            }
            catch (Exception ex)
            {
                return new Result<RegisterPickUpReturn> { Error = ex.Message, StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public async Task<Result<BookingWithRegistration>> GetRegisteredPickUp(Guid bookingId)
        {
            if (bookingId == Guid.Empty)
                return BadRequest<BookingWithRegistration>("Empty Booking Id");
            
            var booking = await _dbContext.Bookings
                            .Where(b => b.BookingId == bookingId)
                            .Join(
                                _dbContext.RegisterPickUpReturns,
                                booking => booking.BookingId,
                                register => register.BookingId,
                                (booking, register) => new BookingWithRegistration
                                {
                                    BookingId = booking.BookingId,
                                    CustomerId = booking.CustomerId,
                                    CarId = booking.CarId,
                                    BookingDate = booking.BookingDate,
                                    RegistrationId = register.RegistrationId,
                                    CustomerSSN = register.CustomerSSN,
                                    PickUp = register.PickUp,
                                    PickUpMeterReading = register.PickUpMeterReading,
                                    Return = register.Return,
                                    ReturnMeterReading = register.ReturnMeterReading
                                })
                            .FirstOrDefaultAsync();

            if (booking == null)
                return NotFound<BookingWithRegistration>($"No booking exist with this bookingId:{bookingId}");

            if (booking.PickUp == null)
                return NotFound<BookingWithRegistration>($"Booking exists but pickup not registered for this bookingId:{bookingId}");

            return new Result<BookingWithRegistration> { Value = booking };
        }

        public async Task<Result<RegisterPickUpReturn>> RegisterReturn(RegisterPickUpReturn registerReturn)
        {
            try
            {
                //RegisterPickUpReturn registeredReturn = new();
                if (registerReturn == null)
                    return BadRequest<RegisterPickUpReturn>("Invalid Request");

                if (string.IsNullOrWhiteSpace(registerReturn.CustomerSSN))
                    return BadRequest<RegisterPickUpReturn>("Customer's social security number (SSN) is missing");

                if (registerReturn is { ReturnMeterReading: null } or { ReturnMeterReading: <= 0 })
                    return BadRequest<RegisterPickUpReturn>("Current meter reading while return is missing or invalid");

                if (registerReturn.ReturnMeterReading < registerReturn.PickUpMeterReading)
                    return BadRequest<RegisterPickUpReturn>("Current meter reading cannot be smaller than the meter reading that was registered while pickup");

                //check for existing registered pickup and if return is not registered yet..
                var booking = await GetRegisteredPickUp(registerReturn.BookingId);

                if (booking is { StatusCode: HttpStatusCode.NotFound })
                    return NotFound<RegisterPickUpReturn>(booking.Error);

                if (booking is { StatusCode: HttpStatusCode.BadRequest })
                    return BadRequest<RegisterPickUpReturn>(booking.Error);

                if (booking is { StatusCode: HttpStatusCode.OK } and { Value.Return: not null })
                    return BadRequest<RegisterPickUpReturn>($"Vehicle has " +
                        $"already been returned by customer: {booking.Value.CustomerSSN} for bookingId: {booking.Value.BookingId}");

                if (booking is { StatusCode: HttpStatusCode.OK, Value.Return: null })
                {
                    _logger.LogInformation("Vehicle return request is valid and is being registered for return");
                    var returnMeterReading = registerReturn.ReturnMeterReading;
                    registerReturn = booking.Value.MapRegisterReturn();
                    registerReturn.ReturnMeterReading = returnMeterReading;
                }

                _dbContext.RegisterPickUpReturns.Update(registerReturn);

                //Update the car's status to Available
                var car = await _dbContext.Cars.FindAsync(booking.Value.CarId);
                car.Status = BookingStatus.Available.ToString();
                car.CurrentMeterReading = registerReturn.ReturnMeterReading.Value;
                _dbContext.Cars.Update(car);

                _dbContext.SaveChanges();

                _logger.LogInformation($"Vehile with regitration number {car.RegistrationNumber} has been returned " +
                    $"with bookingId: {booking.Value.BookingId} by customer: {booking.Value.CustomerSSN}");

                return new Result<RegisterPickUpReturn>
                {
                    Value = registerReturn,
                    StatusCode = HttpStatusCode.Created
                };

            }
            catch (Exception ex)
            {
                return new Result<RegisterPickUpReturn> { Error = ex.Message, StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public async Task<Result<decimal>> GetRentalRates(RentalRatesInput ratesInput)
        {
            try
            {
                if (ratesInput == null)
                    return BadRequest<decimal>("Invalid Request");

                if (ratesInput.BookingId == Guid.Empty)
                    return BadRequest<decimal>("Empty Booking Id");

                if (ratesInput.BaseDayRental <= 0)
                    return BadRequest<decimal>("Invalid Base day rental price");

                if (ratesInput.BaseKmPrice <= 0)
                    return BadRequest<decimal>("Invalid Base KM price");

                // Fetch booking
                var booking = await GetRegisteredPickUp(ratesInput.BookingId);

                // Handle error responses from booking

                if (booking is { StatusCode: HttpStatusCode.NotFound })
                    return NotFound<decimal>(booking.Error);

                if (booking is { StatusCode: HttpStatusCode.BadRequest })
                    return BadRequest<decimal>(booking.Error);

                // Validate pickup and return meter readings
                if (booking is
                    {
                        StatusCode: HttpStatusCode.OK,
                        Value: { ReturnMeterReading: null or <= 0 }
                    })
                {
                    return BadRequest<decimal>($"Vehicle has " +
                        $"already been returned by customer: {booking.Value.CustomerSSN} for bookingId: {booking.Value.BookingId}");
                }

                var car = await _dbContext.Cars.Include(c => c.Category)
                       .FirstAsync(x => x.Id == booking.Value.CarId);

                // Calculate and return rental price 
                var rentalPrice = booking.Value.CalculateRentalPrice(ratesInput.BaseDayRental, ratesInput.BaseKmPrice, car.Category.Category);

                return new Result<decimal>
                {
                    Value = rentalPrice
                };
            }
            catch (Exception ex)
            {
                return new Result<decimal> { Error = ex.Message, StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        private Result<T> BadRequest<T>(string errorMessage)
        {
            _logger.LogWarning(errorMessage);
            return new() { StatusCode = HttpStatusCode.BadRequest, Error = errorMessage };
        }
        private Result<T> NotFound<T>(string errorMessage)
        {
            _logger.LogWarning(errorMessage);
            return new() { StatusCode = HttpStatusCode.NotFound, Error = errorMessage };
        }

    }
}
