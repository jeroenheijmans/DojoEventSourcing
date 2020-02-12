using EventFlow.Commands;

namespace Infi.DojoEventSourcing.Domain.DinnerDeals.Commands
{
    public class CreateDinnerDeal : Command<DinnerDeal, DinnerDeal.DinnerDealId>
    {
        public string VoucherCode { get; }

        public CreateDinnerDeal(DinnerDeal.DinnerDealId aggregateId, string voucherCode)
            : base(aggregateId)
        {
            VoucherCode = voucherCode;
        }
    }
}