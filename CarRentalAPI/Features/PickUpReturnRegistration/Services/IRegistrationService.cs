
using CarRentalAPI.Features.PickUpReturnRegistration.Entities;
using CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects;
using CarRentalAPI.Shared.ValueObjects;

namespace CarRentalAPI.Features.PickUpReturnRegistration.Services
{
    public interface IRegistrationService
    {
        Task<Result<RegisterPickUpReturn>> RegisterPickUp(RegisterPickUpReturn registerPickUp);
        Task<Result<RegisterPickUpReturn>> RegisterReturn(RegisterPickUpReturn registerReturn);
        Task<Result<BookingWithRegistration>> GetRegisteredPickUp(Guid bookingId);
        Task<Result<decimal>> GetRentalRates(RentalRatesInput ratesInput);
        Task<Result<BookingHistoryWithDetails>> GetBookingHistory(Guid bookingId);
    }
}
