using EventFlow.Aggregates;
using EventFlow.EventStores;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;

namespace Infi.DojoEventSourcing.Domain.Reservations.Events
{
    [EventVersion("LineItemCreated", 1)]
    public class LineItemCreated : AggregateEvent<Reservation, ReservationId>
    {
    }
}