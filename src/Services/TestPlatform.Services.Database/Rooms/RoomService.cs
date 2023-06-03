namespace TestPlatform.Services.Database.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.DTOs.BindingModels.Rooms;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Mapper;

    public class RoomService : BaseService<Room>, IRoomService
    {
        private readonly IRoomParticipantMapService roomParticipantMapService;

        public RoomService(IBaseRepository<Room> baseRepository, IMapper mapper,
            IRoomParticipantMapService roomParticipantMapService)
            : base(baseRepository, mapper)
        {
            this.roomParticipantMapService = roomParticipantMapService;
        }

        public IQueryable<T> FindAllRoomsAsQueryable<T>(Guid userId)
        {
            return this.FindAllAsQueryable()
                .Where(r => r.CreatedBy == userId)
                .To<T>();
        }

        public IQueryable<T> FindAllRoomsByUserIdAsQueryable<T>(Guid userId)
        {
            return this.FindAllAsQueryable()
                .Where(r => r.Participants.Any(x => x.UserId == userId))
                .To<T>();
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

        public async Task HardDeleteParticipantsAsync(Guid roomId, IEnumerable<Guid> participantIds)
        {
            var currentParticipants = await this.roomParticipantMapService.FindAllByRoomIdAsync<RoomParticipantMap>(roomId);

            foreach (var participantId in participantIds)
            {
                var participantToRemove = currentParticipants
                    .SingleOrDefault(x => x.UserId == participantId && x.RoomId == roomId);

                await this.roomParticipantMapService.HardDeleteAsync<RoomParticipantMap>(participantToRemove.Id);
            }
        }

        public Task<T> FindRoomByUserIdAndTestIdAsync<T>(Guid userId, Guid testId)
        {
            return this.FindAllAsQueryable()
                .Where(r => r.Participants.Any(a => a.UserId == userId) && r.TestId == testId)
                .To<T>()
                .SingleOrDefaultAsync();
        }
    }
}
