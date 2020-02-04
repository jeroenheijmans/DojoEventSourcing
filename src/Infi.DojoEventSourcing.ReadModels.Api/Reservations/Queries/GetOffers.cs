using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Infi.DojoEventSourcing.Db;

namespace Infi.DojoEventSourcing.ReadModels.Api.Reservations.Queries
{
    public class GetOffers : IQuery<IReadOnlyList<ReservationReadModel>>
    {
    }

    public class GetOffersHandler : IQueryHandler<GetOffers, IReadOnlyList<ReservationReadModel>>
    {
        private readonly IDatabaseContext<IApiReadModelRepositoryFactory> _dbReadContext;

        public GetOffersHandler(IDatabaseContext<IApiReadModelRepositoryFactory> dbReadContext)
        {
            _dbReadContext = dbReadContext;
        }

        public async Task<IReadOnlyList<ReservationReadModel>> ExecuteQueryAsync(
            GetOffers query,
            CancellationToken cancellationToken)
        {
            return await _dbReadContext.RunAsync(factory =>
            {
                var reservationRepo = factory.CreateReservationRepository();
                return reservationRepo.GetAll();
            });
        }
    }
}