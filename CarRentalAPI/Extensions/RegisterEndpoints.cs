using CarRentalAPI.Features.Booking.AggregateRoots;
using CarRentalAPI.Features.Booking.Services;

namespace CarRentalAPI.Extensions
{
    public static class RegisterEndpoints
    {
        public static IEndpointRouteBuilder RouteRegistrationEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("/booking", async (IBookingService bookingService, CarBooking carBooking) =>
            {
                var bookingCreated = await bookingService.CreateBookingAsync(carBooking);
                return Results.Created($"/booking/{bookingCreated.Value.BookingId}", bookingCreated);
            });

            builder.MapGet("/booking", async (IBookingService bookingService, Guid id) =>
            {
                var booking = await bookingService.GetBookingAsync(id);
                return Results.Ok(booking.Value);
            });

            return builder;
        }
    }
}
