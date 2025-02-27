using System.Dynamic;
using System.Net;

namespace CarRentalAPI.Shared.ValueObjects
{
    public record Result<T>
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public T? Value { get; set; }
        public string? Error { get; set; }
    }
    
}
