using System;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;
using NodaMoney;

namespace Infi.DojoEventSourcing.Domain.Reservations.Events
{
    [EventVersion("PriceOffered", 1)]
    public class PriceOffered : AggregateEvent<Reservation, ReservationId>
    {
        public PriceOffered(Guid reservationId, DateTime date, Money price, DateTime expires)
        {
            ReservationId = reservationId;
            Date = date;
            Price = price;
            Expires = expires;
        }

        public Guid ReservationId { get; }
        public DateTime Date { get; }
        public Money Price { get; }
        public DateTime Expires { get; }

        public bool IsInRange(DateTime arrival, DateTime departure) =>
            (Date.Equals(arrival) || Date > arrival)
            && Date < departure;

        public bool HasExpired(Clock clock) => !IsStillValid(clock);

        public bool IsStillValid(Clock clock) => Expires > clock.instant();
    }
}