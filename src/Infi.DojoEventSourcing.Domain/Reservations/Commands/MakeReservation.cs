using EventFlow.Commands;
using EventFlow.Core;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;

namespace Infi.DojoEventSourcing.Domain.Reservations.Commands
{
    public class MakeReservation : Command<Reservation, ReservationId>
    {
        public MakeReservation(ReservationId aggregateId, string number)
            : base(aggregateId)
        {
            Number = number;
        }

        public MakeReservation(ReservationId aggregateId, string number, ISourceId sourceId)
            : base(aggregateId, sourceId)
        {
            Number = number;
        }

        public string Number { get; }
    }
}