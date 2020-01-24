using EventFlow.Aggregates;
using EventFlow.EventStores;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;

namespace Infi.DojoEventSourcing.Domain.Reservations.Events
{
    [EventVersion("RoomAssigned", 1)]
    public class RoomAssigned : AggregateEvent<Reservation, ReservationId>
    {
    }
}