namespace TestPlatform.Services.Database.Rooms
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;

    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Mapper;

    public class RoomParticipantMapService : BaseService<RoomParticipantMap>, IRoomParticipantMapService
    {
        public RoomParticipantMapService(IBaseRepository<RoomParticipantMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }

        public async Task<IEnumerable<T>> FindAllByRoomIdAsync<T>(Guid roomId)
        {
            var roomUserMaps = await this.BaseRepository.GetAllAsQueryable()
                .Where(rpm => rpm.RoomId == roomId)
                .To<T>()
                .ToListAsync();

            return roomUserMaps;
        }
    }
}
