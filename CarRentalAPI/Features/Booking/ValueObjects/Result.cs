using System.Net;

namespace CarRentalAPI.Features.Booking.ValueObjects
{
    public class Result<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public T Value { get; set; }
        public string? Error { get; set; }
    }
}
