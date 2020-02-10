using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using EventFlow.ReadStores;
using Infi.DojoEventSourcing.Domain.Reservations.Events;
using Infi.DojoEventSourcing.Domain.Reservations.Queries;

namespace ReadModelRebuilder
{
    public class ReadmodelRebuildService
    {
        private readonly IReadModelPopulator _readModelPopulator;
        private readonly IEventDefinitionService _eventDefinitionService;


        public ReadmodelRebuildService(
            IReadModelPopulator readModelPopulator,
            IEventDefinitionService eventDefinitionService)
        {
            _readModelPopulator = readModelPopulator;
            _eventDefinitionService = eventDefinitionService;
        }

        public async Task RebuildAllReadModels(CancellationToken cancellationToken)
        {
            // FIXME BS This is a bug in EventFlow
            RegisterAllEvents();

            // TODO BS Rebuild all read models
            await _readModelPopulator.PurgeAsync(typeof(ReservationReadModel), cancellationToken);
            await _readModelPopulator.PopulateAsync(typeof(ReservationReadModel), cancellationToken);
        }

        private void RegisterAllEvents()
        {
            var eventAssembly = typeof(ReservationCreated).Assembly;
            var aggregateEventTypes = eventAssembly
                .GetTypes()
                .Where(t => IsAssignableToGenericType(t, typeof(IAggregateEvent<,>)))
                .ToImmutableList();
            _eventDefinitionService.Load(aggregateEventTypes);
        }

        private static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            var baseType = givenType.BaseType;
            return baseType != null && IsAssignableToGenericType(baseType, genericType);
        }
    }
}