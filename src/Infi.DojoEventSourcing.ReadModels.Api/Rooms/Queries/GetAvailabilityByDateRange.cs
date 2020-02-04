using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Infi.DojoEventSourcing.Db;
using Infi.DojoEventSourcing.Domain.Rooms.Queries;

namespace Infi.DojoEventSourcing.ReadModels.Api.Rooms.Queries
{
    public class GetAvailabilityByDateRange : IQuery<IReadOnlyList<RoomAvailabilityDto>>
    {
        public GetAvailabilityByDateRange(DateTime startDateUtc, DateTime endDateUtc)
        {
            StartDateUtc = startDateUtc;
            EndDateUtc = endDateUtc;
        }

        public DateTime StartDateUtc { get; }
        public DateTime EndDateUtc { get; }
    }

    public class GetAvailabilityByDateRangeHandler
        : IQueryHandler<GetAvailabilityByDateRange, IReadOnlyList<RoomAvailabilityDto>>
    {
        private readonly IDatabaseContext<IApiReadModelRepositoryFactory> _dbReadContext;

        public GetAvailabilityByDateRangeHandler(IDatabaseContext<IApiReadModelRepositoryFactory> dbReadContext)
        {
            _dbReadContext = dbReadContext;
        }

        public async Task<IReadOnlyList<RoomAvailabilityDto>> ExecuteQueryAsync(
            GetAvailabilityByDateRange query,
            CancellationToken cancellationToken)
        {
            var rooms = await GetAllRooms();
            var roomOccupations = await GetAllRoomOccupationsFor(query);

            var roomAvailabilities = new List<RoomAvailabilityDto>();

            foreach (var room in rooms)
            {
                var roomAvailability = new RoomAvailabilityDto
                {
                    RoomId = new Guid(room.AggregateId),
                    RoomNumber = room.RoomNumber,
                };

                var occupationsForRoom = roomOccupations
                    .Where(o => o.AggregateId == room.AggregateId)
                    .ToImmutableList();

                var occupiedIntervals = new List<RoomAvailabilityIntervalDto>();
                foreach (var roomOccupation in occupationsForRoom)
                {
                    occupiedIntervals.Add(new RoomAvailabilityIntervalDto
                    {
                        Start = roomOccupation.StartDate,
                        End = roomOccupation.EndDate,
                        IsOccupied = true,
                    });
                }

                var roomAvailabilityIntervals = new List<RoomAvailabilityIntervalDto>(occupiedIntervals);
                var previousEnd = query.StartDateUtc;
                foreach (var occupiedInterval in occupiedIntervals)
                {
                    if (previousEnd < occupiedInterval.Start)
                    {
                        roomAvailabilityIntervals.Add(new RoomAvailabilityIntervalDto
                        {
                            Start = previousEnd,
                            End = occupiedInterval.Start,
                            IsOccupied = false,
                        });
                    }

                    previousEnd = occupiedInterval.End;
                }

                if (previousEnd < query.EndDateUtc)
                {
                    // there is a gap after the last interval
                    roomAvailabilityIntervals.Add(new RoomAvailabilityIntervalDto
                    {
                        Start = previousEnd,
                        End = query.EndDateUtc,
                        IsOccupied = false
                    });
                }

                roomAvailability.Details = roomAvailabilityIntervals.OrderBy(r => r.Start).ToList();

                roomAvailability.IsAvailable =
                    roomAvailability.Details.Count == 1
                    && !roomAvailability.Details.Single().IsOccupied;

                roomAvailabilities.Add(roomAvailability);
            }

            return roomAvailabilities;
        }

        private async Task<IReadOnlyList<RoomOccupationReadModel>> GetAllRoomOccupationsFor(
            GetAvailabilityByDateRange query)
        {
            return await _dbReadContext.RunAsync(factory =>
            {
                var roomRepository = factory.CreateRoomRepository();
                return roomRepository.GetOccupationsBetween(query.StartDateUtc, query.EndDateUtc);
            });
        }

        private async Task<IReadOnlyList<RoomReadModel>> GetAllRooms()
        {
            return await _dbReadContext.RunAsync(factory =>
            {
                var roomRepository = factory.CreateRoomRepository();
                return roomRepository.GetAll();
            });
        }
    }
}