using CarRentalAPI.Features.Booking.AggregateRoots;
using CarRentalAPI.Features.PickUpReturnRegistration.AggregateRoots;
using CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects;
using CarRentalAPI.Shared.ValueObjects;

namespace CarRentalAPI.Features.PickUpReturnRegistration.Services
{
    public interface IRegistrationService
    {
        Task<Result<RegisterPickUpReturn>> RegisterPickUp(RegisterPickUpReturn registerPickUp);
        Task<Result<RegisterPickUpReturn>> RegisterReturn(RegisterPickUpReturn registerReturn);
        Task<Result<CarBooking>> GetRegisteredPickUp(Guid bookingId);
        Task<Result<decimal>> GetRentalRates(RentalRatesInput ratesInput);
    }
}
