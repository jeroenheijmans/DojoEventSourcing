using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Infi.DojoEventSourcing.Domain.DinnerDeals.Events
{
    [EventVersion("DinnerDealCreated", 1)]
    public class DinnerDealCreated : IAggregateEvent<DinnerDeal, DinnerDeal.DinnerDealId>
    {
        public string VoucherCode { get; }

        public DinnerDealCreated(string voucherCode)
        {
            VoucherCode = voucherCode;
        }
    }
}