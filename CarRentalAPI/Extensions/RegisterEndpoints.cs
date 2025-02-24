using CarRentalAPI.Features.Booking.AggregateRoots;
using CarRentalAPI.Features.Booking.Services;
using CarRentalAPI.Features.Booking.ValueObjects;

namespace CarRentalAPI.Extensions
{
    public static class RegisterEndpoints
    {
        public static IEndpointRouteBuilder RouteRegistrationEndpoints(this IEndpointRouteBuilder builder)
        {            
            builder.MapPost("/booking/", async (IBookingService bookingService, BookingRequest request) =>
            {

                var bookingCreated = await bookingService.CreateBookingAsync(request);
                return bookingCreated.StatusCode == System.Net.HttpStatusCode.Created ?
                    Results.Created($"/booking/{bookingCreated.Value.BookingId}", bookingCreated.Value) :
                    Results.NotFound(bookingCreated.Error);
            });

            builder.MapGet("/booking/{id}", async (IBookingService bookingService, Guid id) =>
            {
                var booking = await bookingService.GetBookingAsync(id);
                return booking.StatusCode == System.Net.HttpStatusCode.OK ? Results.Ok(booking.Value) :
                    Results.NotFound(booking.Error);
            });

            builder.MapPut("/booking/{id}", async (IBookingService bookingService, Guid id, CarBooking carBooking) =>
            {
                var booking = await bookingService.UpdateBookingAsync(id, carBooking);
                return booking.StatusCode == System.Net.HttpStatusCode.OK ? Results.Ok(booking.Value) :
                    Results.NotFound(booking.Error);
            });

            builder.MapDelete("/booking/{id}", async (IBookingService bookingService, Guid id) =>
            {
                var deleted = await bookingService.DeleteBookingAsync(id);
                return deleted ? Results.Ok(deleted) : Results.NoContent();
            });

            return builder;
        }
    }
}
