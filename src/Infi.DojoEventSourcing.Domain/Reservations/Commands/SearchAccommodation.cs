using EventFlow.Commands;
using EventFlow.Core;
using Infi.DojoEventSourcing.Domain.Rooms;

namespace Infi.DojoEventSourcing.Domain.Reservations.Commands
{
    public class SearchAccommodation : Command<Room, Room.RoomIdentity>
    {
        public SearchAccommodation(Room.RoomIdentity aggregateId) : base(aggregateId)
        {
        }

        public SearchAccommodation(Room.RoomIdentity aggregateId, ISourceId sourceId) : base(aggregateId, sourceId)
        {
        }
    }
}