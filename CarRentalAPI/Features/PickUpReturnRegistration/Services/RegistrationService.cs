using CarRentalAPI.Data;
using CarRentalAPI.Features.Booking.AggregateRoots;
using CarRentalAPI.Features.Booking.ValueObjects;
using CarRentalAPI.Features.PickUpReturnRegistration.AggregateRoots;
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

                if (registerPickUp.PickUpMeterReading <= 0)
                    return BadRequest<RegisterPickUpReturn>("Current meter reading while pickup is missing or invalid");

                if (string.IsNullOrWhiteSpace(registerPickUp.CustomerSSN))
                    return BadRequest<RegisterPickUpReturn>("Customer's social security number (SSN) is missing");

                //check if booking exist for the customer?
                var bookingByCustomer = await _dbContext.Bookings.FindAsync(registerPickUp.BookingId);
                if (bookingByCustomer is null)
                    return BadRequest<RegisterPickUpReturn>($"Can't find any booking by customer: {registerPickUp.CustomerSSN}");

                //check if pickup was already registered?
                var pickUp = await GetRegisteredPickUp(registerPickUp.BookingId);
                if (pickUp.StatusCode == HttpStatusCode.OK)
                {
                    return BadRequest<RegisterPickUpReturn>($"Pickup already registered for booking: {registerPickUp.BookingId}");
                }

                _logger.LogInformation("Vehicle pickup request is valid and is being registered for pickup");
                registerPickUp.PickUp = DateTime.UtcNow;
                registerPickUp.CreatedAt = DateTime.UtcNow;

                var pickUpRegistered = await _dbContext.RegisterPickUpReturns.AddAsync(registerPickUp);
                await _dbContext.SaveChangesAsync();
                return new Result<RegisterPickUpReturn> { Value = pickUpRegistered.Entity };

            }
            catch (Exception ex)
            {
                return new Result<RegisterPickUpReturn> { Error = ex.Message, StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public async Task<Result<CarBooking>> GetRegisteredPickUp(Guid bookingId)
        {
            if (bookingId == Guid.Empty)
                return BadRequest<CarBooking>("Empty Booking Id");

            var booking = _dbContext.Bookings
                            .Include(b => b.RegisterPickUpReturn)
                            .FirstOrDefault(b => b.BookingId == bookingId);

            if (booking == null)
                return NotFound<CarBooking>($"No booking exist with this bookingId:{bookingId}");

            if (booking.RegisterPickUpReturn == null)
                return NotFound<CarBooking>($"Booking exists but pickup not registered for this bookingId:{bookingId}");

            return new Result<CarBooking> { Value = booking };
        }

        public async Task<Result<RegisterPickUpReturn>> RegisterReturn(RegisterPickUpReturn registerReturn)
        {
            try
            {
                RegisterPickUpReturn registeredReturn = new();
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

                if (booking is { StatusCode: HttpStatusCode.OK } and { Value.RegisterPickUpReturn.Return: not null })
                    return BadRequest<RegisterPickUpReturn>($"Vehicle with registration number: {booking.Value.Car.RegistrationNumber} has " +
                        $"already been returned by customer: {booking.Value.Customer.SSN}");

                if (booking is { StatusCode: HttpStatusCode.OK, Value.RegisterPickUpReturn.Return: null })
                {
                    _logger.LogInformation("Vehicle return request is valid and is being registered for return");
                    registeredReturn = booking.Value.RegisterPickUpReturn.MapRegisterReturn();
                }

                _dbContext.RegisterPickUpReturns.Update(registeredReturn);

                //Update the car's status to Available
                var car = await _dbContext.Cars.FindAsync(booking.Value.Car.Id);
                car.Status = BookingStatus.Available.ToString();
                _dbContext.Cars.Update(car);

                _dbContext.SaveChanges();

                _logger.LogInformation($"Vehile with regitration number {booking.Value.Car.RegistrationNumber} has been returned " +
                    $"with bookingId: {booking.Value.BookingId} by customer: {booking.Value.Customer.SSN}");

                return new Result<RegisterPickUpReturn>
                {
                    Value = registeredReturn
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
                        Value.RegisterPickUpReturn: { ReturnMeterReading: null or <= 0 }
                    })
                {
                    return BadRequest<decimal>($"Vehicle with registration number: {booking.Value.Car.RegistrationNumber} has " +
                        $"not been returned by customer: {booking.Value.Customer.SSN}");
                }

                // Calculate and return rental price 
                var rentalPrice = booking.Value.RegisterPickUpReturn.CalculateRentalPrice(ratesInput.BaseDayRental, ratesInput.BaseKmPrice);
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
