using CarRentalAPI.Features.PickUpReturnRegistration.Services;
using CarRentalAPI.Features.PickUpReturnRegistration.ValueObjects;
using CarRentalAPI.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Features.PickUpReturnRegistration.Extensions
{
    public static class RegistrationEndpoints
    {
        public static IEndpointRouteBuilder RouteRegistrationEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("/pickUpRegistration/", async ([FromServices] IRegistrationService registrationService, [FromBody] PickUpReturnRequest request) =>
            {

                var pickUpRegistered = await registrationService.RegisterPickUp(request.MapPickUpReturnRequest());
                return pickUpRegistered.StatusCode switch
                {
                    System.Net.HttpStatusCode.Created => Results.Created($"/pickUpReturnRegistration/{pickUpRegistered.Value.BookingId}", pickUpRegistered.Value),
                    System.Net.HttpStatusCode.NotFound => Results.NotFound(pickUpRegistered.Error),
                    System.Net.HttpStatusCode.BadRequest => Results.BadRequest(pickUpRegistered.Error),
                    System.Net.HttpStatusCode.InternalServerError => Results.InternalServerError(pickUpRegistered.Error)
                };
            });

            builder.MapGet("/pickUpReturnRegistration/{id}", async ([FromServices] IRegistrationService registrationService, Guid id) =>
            {
                var pickUpRegistration = await registrationService.GetRegisteredPickUp(id);
                return pickUpRegistration.StatusCode switch
                {
                    System.Net.HttpStatusCode.OK => Results.Ok(pickUpRegistration.Value),
                    System.Net.HttpStatusCode.NotFound => Results.NotFound(pickUpRegistration.Error),
                    System.Net.HttpStatusCode.BadRequest => Results.BadRequest(pickUpRegistration.Error),
                    System.Net.HttpStatusCode.InternalServerError => Results.InternalServerError(pickUpRegistration.Error)
                };
            });

            builder.MapPut("/returnRegistration/{id}", async ([FromServices] IRegistrationService registrationService, [FromBody] PickUpReturnRequest request) =>
            {
                var returnRegistered = await registrationService.RegisterReturn(request.MapPickUpReturnRequest());
                return returnRegistered.StatusCode switch
                {
                    System.Net.HttpStatusCode.Created => Results.Created($"/pickUpReturnRegistration/{returnRegistered.Value.BookingId}", returnRegistered.Value),
                    System.Net.HttpStatusCode.NotFound => Results.NotFound(returnRegistered.Error),
                    System.Net.HttpStatusCode.BadRequest => Results.BadRequest(returnRegistered.Error),
                    System.Net.HttpStatusCode.InternalServerError => Results.InternalServerError(returnRegistered.Error)
                };
            });

            builder.MapPost("/calculateRentalPrice/{id}", async ([FromServices] IRegistrationService registrationService, [FromBody] RentalRatesInput input) =>
            {
                var pickUpRegistration = await registrationService.GetRentalRates(input);
                return pickUpRegistration.StatusCode switch
                {
                    System.Net.HttpStatusCode.OK => Results.Ok(pickUpRegistration.Value), 
                    System.Net.HttpStatusCode.NotFound => Results.NotFound(pickUpRegistration.Error),
                    System.Net.HttpStatusCode.BadRequest => Results.BadRequest(pickUpRegistration.Error),
                    System.Net.HttpStatusCode.InternalServerError => Results.InternalServerError(pickUpRegistration.Error)
                };
            });

            builder.MapGet("/getBookingHistory/{bookingId}", async ([FromServices] IRegistrationService registrationService, Guid bookingId) =>
            {
                var bookingHistory = await registrationService.GetBookingHistory(bookingId);
                return bookingHistory.StatusCode switch
                {
                    System.Net.HttpStatusCode.OK => Results.Ok(bookingHistory.Value),
                    System.Net.HttpStatusCode.NotFound => Results.NotFound(bookingHistory.Error),
                    System.Net.HttpStatusCode.BadRequest => Results.BadRequest(bookingHistory.Error),
                    System.Net.HttpStatusCode.InternalServerError => Results.InternalServerError(bookingHistory.Error)
                };
            });

            return builder;
        }
    }
}
