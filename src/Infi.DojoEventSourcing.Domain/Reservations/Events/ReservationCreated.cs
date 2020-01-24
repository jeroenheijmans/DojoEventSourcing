using EventFlow.Aggregates;
using EventFlow.EventStores;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;

namespace Infi.DojoEventSourcing.Domain.Reservations.Events
{
    [EventVersion("ReservationCreated", 1)]
    public class ReservationCreated : AggregateEvent<Reservation, ReservationId>
    {
    }
}