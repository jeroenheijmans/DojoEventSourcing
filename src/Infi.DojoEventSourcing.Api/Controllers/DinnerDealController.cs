using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Infi.DojoEventSourcing.Domain.DinnerDeals;
using Infi.DojoEventSourcing.Domain.DinnerDeals.Commands;
using Infi.DojoEventSourcing.Domain.DinnerDeals.Events;
using Infi.DojoEventSourcing.Domain.Reservations.Commands;
using Infi.DojoEventSourcing.Domain.Reservations.Queries;
using Infi.DojoEventSourcing.Domain.Reservations.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using static Infi.DojoEventSourcing.Domain.DinnerDeals.DinnerDeal;

namespace DojoEventSourcing.Controllers
{
    [Route("[controller]")]
    public class DinnerDealController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public DinnerDealController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("for-reservation/{reservationId}")]
        public async Task<IActionResult> GetForReservation(Guid reservationGuid)
        {
            var reservationId = ReservationId.With(reservationGuid);
            var reservation = _queryProcessor.Process(new FindReservationById(reservationId), CancellationToken.None);

            // create different voucher based on reservation, e.g. dates?
            var voucher = DinnerDeal.DealVoucherCodes.FreeDrink;
            var id = DinnerDealId.New;

            var dealCreatedOrError = await _commandBus.PublishAsync(
                new CreateDinnerDeal(id, voucher), default(CancellationToken)
            );

            if (dealCreatedOrError.IsSuccess)
            {
                return Json(id.GetGuid());
            }

            return BadRequest(dealCreatedOrError);
        }
    }
}