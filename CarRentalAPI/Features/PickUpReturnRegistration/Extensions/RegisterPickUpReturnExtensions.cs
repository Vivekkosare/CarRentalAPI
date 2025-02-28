using CarRentalAPI.Features.PickUpReturnRegistration.Entities;
using CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects;
using CarRentalAPI.Shared.Entities;
using System.Runtime.CompilerServices;

namespace CarRentalAPI.Features.PickUpReturnRegistration.Extensions
{
    public static class RegisterPickUpReturnExtensions
    {
        public static RegisterPickUpReturn MapRegisterReturn(this BookingWithRegistration existingPickUpReturn)
        {
            RegisterPickUpReturn registerPickUpReturn = new();
            registerPickUpReturn.PickUp = existingPickUpReturn.PickUp.Value;
            registerPickUpReturn.BookingId = existingPickUpReturn.BookingId.Value;
            registerPickUpReturn.RegistrationId = existingPickUpReturn.RegistrationId.Value;
            registerPickUpReturn.PickUpMeterReading = existingPickUpReturn.PickUpMeterReading.Value;
            registerPickUpReturn.CreatedAt = existingPickUpReturn.PickUp.Value;
            registerPickUpReturn.CustomerSSN = existingPickUpReturn.CustomerSSN;
            registerPickUpReturn.ReturnMeterReading = existingPickUpReturn.ReturnMeterReading?? existingPickUpReturn.ReturnMeterReading;

            registerPickUpReturn.Return = DateTime.UtcNow;
            registerPickUpReturn.UpdatedAt = DateTime.UtcNow;
            return registerPickUpReturn;
        }

        public static decimal CalculateRentalPrice(this BookingWithRegistration registerPickUpReturn, decimal baseDayRental, decimal baseKmPrice, string carCategory)
        {
            int numberOfDays = (registerPickUpReturn.Return.Value - registerPickUpReturn.PickUp.Value).Days;
            int numberOfKMs = (registerPickUpReturn.ReturnMeterReading.Value - registerPickUpReturn.PickUpMeterReading.Value);

            return carCategory.ToLower() switch
            {
                "small" => baseDayRental * numberOfDays,
                "medium" => (baseDayRental * numberOfDays * (decimal)1.3) + baseKmPrice * numberOfKMs,
                "large" => (baseDayRental * numberOfDays * (decimal)1.5) + baseKmPrice * numberOfKMs,
                "suv" => (baseDayRental * numberOfDays * (decimal)1.5) + baseKmPrice * numberOfKMs * (decimal)1.5,
                "minivan" => (baseDayRental * numberOfDays * (decimal)1.7) + baseKmPrice * numberOfKMs * (decimal)1.7,
                _ => 0
            };
        }

        public static RegisterPickUpReturn MapPickUpReturnRequest(this PickUpReturnRequest request) =>
             new RegisterPickUpReturn
             {
                 BookingId = request.BookingId,
                 CustomerSSN = request.CustomerSSN,
                 ReturnMeterReading = request.ReturnMeterReading
             };
    }
}
