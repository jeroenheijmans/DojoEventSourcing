using EventFlow.Aggregates;
using EventFlow.EventStores;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;

namespace Infi.DojoEventSourcing.Domain.Reservations.Events
{
    [EventVersion("SearchedForAccommodation", 1)]
    public class SearchedForAccommodation : AggregateEvent<Reservation, ReservationId>
    {
    }
}