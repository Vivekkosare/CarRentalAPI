using CarRentalAPI.Features.PickUpReturnRegistration.AggregateRoots;
using CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects;
using CarRentalAPI.Shared.Entities;
using System.Runtime.CompilerServices;

namespace CarRentalAPI.Features.PickUpReturnRegistration.Extensions
{
    public static class RegisterPickUpReturnExtensions
    {
        public static RegisterPickUpReturn MapRegisterReturn(this RegisterPickUpReturn existingPickUpReturn)
        {
            RegisterPickUpReturn registerPickUpReturn = new();
            registerPickUpReturn.PickUp = existingPickUpReturn.PickUp;
            registerPickUpReturn.BookingId = existingPickUpReturn.BookingId;
            registerPickUpReturn.RegistrationId = existingPickUpReturn.RegistrationId;
            registerPickUpReturn.PickUpMeterReading = existingPickUpReturn.PickUpMeterReading;
            registerPickUpReturn.ReturnMeterReading = existingPickUpReturn.ReturnMeterReading;
            registerPickUpReturn.CreatedAt = existingPickUpReturn.CreatedAt;

            registerPickUpReturn.Return = DateTime.UtcNow;
            registerPickUpReturn.UpdatedAt = DateTime.UtcNow;
            return registerPickUpReturn;
        }

        public static decimal CalculateRentalPrice(this RegisterPickUpReturn registerPickUpReturn, decimal baseDayRental, decimal baseKmPrice)
        {
            int numberOfDays = (registerPickUpReturn.Return.Value - registerPickUpReturn.PickUp).Days;
            int numberOfKMs = (registerPickUpReturn.ReturnMeterReading.Value - registerPickUpReturn.PickUpMeterReading);

            return registerPickUpReturn.Booking.Car.Category.Category.ToLower() switch
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
                 CustomerSSN = request.CustomerSSN
             };
    }
}
