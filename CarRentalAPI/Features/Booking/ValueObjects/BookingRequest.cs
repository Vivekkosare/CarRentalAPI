﻿namespace CarRentalAPI.Features.Booking.ValueObjects
{
    public record BookingRequest
    {
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
