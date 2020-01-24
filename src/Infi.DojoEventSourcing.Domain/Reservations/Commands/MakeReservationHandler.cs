using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;

namespace Infi.DojoEventSourcing.Domain.Reservations.Commands
{
    public class MakeReservationHandler : CommandHandler<Reservation, ReservationId, MakeReservation>
    {
        public override Task ExecuteAsync(
            Reservation reservation,
            MakeReservation command,
            CancellationToken cancellationToken)
        {
            reservation.Create(command.Number);

            return Task.FromResult(0); // FIXME ED Magic number
        }
    }
}