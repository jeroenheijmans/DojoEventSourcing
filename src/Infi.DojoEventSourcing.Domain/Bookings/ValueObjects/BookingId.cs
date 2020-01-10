using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Infi.DojoEventSourcing.Domain.Bookings.ValueObjects
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class BookingId : Identity<BookingId>
    {
        public BookingId(string value)
            : base(value)
        {
        }
    }
}