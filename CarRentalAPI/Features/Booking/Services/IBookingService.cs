﻿using CarRentalAPI.Features.Booking.Entities;
using CarRentalAPI.Features.Booking.ValueObjects;
using CarRentalAPI.Shared.ValueObjects;

namespace CarRentalAPI.Features.Booking.Services
{
    public interface IBookingService
    {
        Task<Result<CarBooking>> CreateBookingAsync(BookingRequest request);
        Task<Result<CarBooking>> GetBookingAsync(Guid id);
        Task<Result<CarBooking>> UpdateBookingAsync(Guid id, CarBooking booking);
        Task<bool> DeleteBookingAsync(Guid Id);


    }
}
