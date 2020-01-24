using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using Infi.DojoEventSourcing.Domain.Rooms;

namespace Infi.DojoEventSourcing.Domain.Reservations.Commands
{
    public class 
    SearchAccommodationHandler : CommandHandler<Room, Room.RoomIdentity, SearchAccommodation>
    {
        private readonly IRoomRepository _roomRepository;

        public SearchAccommodationHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override Task ExecuteAsync(Room room, SearchAccommodation command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}