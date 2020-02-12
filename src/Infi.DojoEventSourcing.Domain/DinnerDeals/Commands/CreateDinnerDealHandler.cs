using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using LanguageExt;
using Serilog;

namespace Infi.DojoEventSourcing.Domain.DinnerDeals.Commands
{
    public class CreateDinnerDealHandler : CommandHandler<
        DinnerDeal,
        DinnerDeal.DinnerDealId,
        IExecutionResult,
        CreateDinnerDeal>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(
            DinnerDeal aggregate,
            CreateDinnerDeal command,
            CancellationToken cancellationToken)
        {
            try
            {
                aggregate.Create(command.VoucherCode);
                return ExecutionResult.Success().AsTask();
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not create dinner deal. {command}", command);
                return ExecutionResult.Failed(e.Message).AsTask();
            }
        }
    }
}