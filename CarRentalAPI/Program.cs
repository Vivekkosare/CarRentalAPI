using CarRentalAPI.Data;
using CarRentalAPI.Features.Booking.Extensions;
using CarRentalAPI.Features.Booking.Services;
using CarRentalAPI.Features.PickUpReturnRegistration.Extensions;
using CarRentalAPI.Features.PickUpReturnRegistration.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddLogging();
builder.Services.AddDbContext<CarDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(static endpoints => endpoints.RouteBookingEndpoints());
app.UseEndpoints(static endpoints => endpoints.RouteRegistrationEndpoints());

app.Run();
