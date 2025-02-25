using CarRentalAPI.Features.PickUpReturnRegistration.AggregateRoots;

namespace CarRentalAPI.Features.PickUpReturnRegistration.Services
{
    public interface IRegistrationService
    {
        Task<RegisterPickUpReturn> RegisterPickUp(RegisterPickUpReturn registerPickUp);
        Task<RegisterPickUpReturn> RegisterReturn(RegisterPickUpReturn registerReturn);
    }
}
