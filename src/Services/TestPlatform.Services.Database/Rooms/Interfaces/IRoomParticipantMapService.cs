namespace TestPlatform.Services.Database.Rooms.Interfaces
{
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Database.Interfaces;

    public interface IRoomParticipantMapService : IBaseService<RoomParticipantMap>
    {
        Task<IEnumerable<T>> FindAllByRoomIdAsync<T>(Guid roomId);
    }
}
