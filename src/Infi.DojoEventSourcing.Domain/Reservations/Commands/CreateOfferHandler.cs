using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using Infi.DojoEventSourcing.Domain.Pricings;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;
using LanguageExt;
using Serilog;

namespace Infi.DojoEventSourcing.Domain.Reservations.Commands
{
    public class CreateOfferHandler : CommandHandler<Reservation, ReservationId, IExecutionResult, CreateOffer>
    {
        private readonly IPricingEngine _pricing;

        public CreateOfferHandler(IPricingEngine pricing)
        {
            _pricing = pricing;
        }

        public override Task<IExecutionResult> ExecuteCommandAsync(
            Reservation reservation,
            CreateOffer command,
            CancellationToken cancellationToken)
        {
            try
            {
                // reservation.discoverCustomer(); // FIXME
                reservation.CreateOffers(command.Arrival, command.Departure, _pricing);
                return ExecutionResult.Success().AsTask();
            }
            catch (Exception e)
            {
                Log.Error(e,
                    "Failed to create offers for {reservationId}: {error}",
                    reservation.Id,
                    e.Message);
                return ExecutionResult.Failed(e.Message).AsTask();
            }
        }
    }
}