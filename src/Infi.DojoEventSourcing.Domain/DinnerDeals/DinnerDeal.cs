using System.Text.Json.Serialization;
using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.ValueObjects;
using Infi.DojoEventSourcing.Domain.DinnerDeals.Events;

namespace Infi.DojoEventSourcing.Domain.DinnerDeals 
{
    public class DinnerDeal :
        AggregateRoot<DinnerDeal, DinnerDeal.DinnerDealId>,
        IApply<DinnerDealCreated>        
    {
        [JsonConverter(typeof(SingleValueObjectConverter))]
        public class DinnerDealId : Identity<DinnerDealId>
        {
            public DinnerDealId(string value) : base(value)
            { }
        }

        public DinnerDeal(DinnerDealId id) : base(id)
        { }

        public string DealVoucherCode { get; set; }

        public static class DealVoucherCodes
        {
            public const string FreeDrink = "FREE-DRINK-PER-PERSON";
            public const string Discount = "DISCOUNT-10-PERCENT";
            // etc.
        }

        public void Create(string voucherCode)
        {
            Emit(new DinnerDealCreated(voucherCode));
        }

        public void Apply(DinnerDealCreated aggregateEvent)
        {
            this.DealVoucherCode = aggregateEvent.VoucherCode;
        }
    }
}