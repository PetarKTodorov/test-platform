namespace TestPlatform.Services.Database.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;

    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.DTOs.BindingModels.Rooms;
    using TestPlatform.Services.Database.Rooms.Interfaces;

    public class RoomService : BaseService<Room>, IRoomService
    {
        private readonly IRoomParticipantMapService roomParticipantMapService;

        public RoomService(IBaseRepository<Room> baseRepository, IMapper mapper,
            IRoomParticipantMapService roomParticipantMapService)
            : base(baseRepository, mapper)
        {
            this.roomParticipantMapService = roomParticipantMapService;
        }

        public async Task UpdateParticipantsAsync(Guid roomId, IEnumerable<Guid> participantIds, Guid currentUserId)
        {
            var currentParticipants = await this.roomParticipantMapService.FindAllByRoomIdAsync<RoomParticipantMap>(roomId);
            var currentParticipantIds = currentParticipants.Select(tsm => tsm.UserId);

            var participantIdsToRemove = currentParticipantIds.Except(participantIds);
            var participantIdsToAdd = participantIds.Except(currentParticipantIds);

            foreach (var participantId in participantIdsToRemove)
            {
                var participantToRemove = currentParticipants
                    .SingleOrDefault(x => x.UserId == participantId && x.RoomId == roomId);

                await this.roomParticipantMapService.HardDeleteAsync<RoomParticipantMap>(participantToRemove.Id);
            }

            foreach (var participantId in participantIdsToAdd)
            {
                var newRoomParticipantMap = new CreateRoomParticipantMapBM()
                {
                    RoomId = roomId,
                    UserId = participantId
                };

                await this.roomParticipantMapService.CreateAsync<RoomParticipantMap, CreateRoomParticipantMapBM>(newRoomParticipantMap, currentUserId);
            }
        }
    }
}
