﻿using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using Infi.DojoEventSourcing.Domain.Commands.Reservations;
using Infi.DojoEventSourcing.Domain.Reservations;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;
using LanguageExt;

namespace Infi.DojoEventSourcing.Domain.CommandHandlers.Reservations
{
    public class PlaceReservationHandler
        : CommandHandler<Reservation, ReservationId, IExecutionResult, PlaceReservation>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            Reservation reservation,
            PlaceReservation command,
            CancellationToken cancellationToken)
        {
            reservation.Place();

            return await new SuccessExecutionResult().AsTask();
        }
    }
}