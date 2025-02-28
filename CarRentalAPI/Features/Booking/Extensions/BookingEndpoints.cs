using CarRentalAPI.Features.Booking.Entities;
using CarRentalAPI.Features.Booking.Services;
using CarRentalAPI.Features.Booking.ValueObjects;

namespace CarRentalAPI.Features.Booking.Extensions
{
    public static class BookingEndpoints
    {
        public static IEndpointRouteBuilder RouteBookingEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("/booking/", async (IBookingService bookingService, BookingRequest request) =>
            {

                var bookingCreated = await bookingService.CreateBookingAsync(request);
                return bookingCreated.StatusCode switch
                {
                    System.Net.HttpStatusCode.Created => Results.Created($"/booking/{bookingCreated.Value.BookingId}", bookingCreated.Value),
                    System.Net.HttpStatusCode.NotFound => Results.NotFound(bookingCreated.Error),
                    System.Net.HttpStatusCode.BadRequest => Results.BadRequest(bookingCreated.Error),
                };
            });

            builder.MapGet("/booking/{id}", async (IBookingService bookingService, Guid id) =>
            {
                var booking = await bookingService.GetBookingAsync(id);
                return booking.StatusCode switch
                {
                    System.Net.HttpStatusCode.OK => Results.Ok(booking.Value),
                    System.Net.HttpStatusCode.NotFound => Results.NotFound(booking.Error),
                    System.Net.HttpStatusCode.BadRequest => Results.BadRequest(booking.Error),
                };
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
