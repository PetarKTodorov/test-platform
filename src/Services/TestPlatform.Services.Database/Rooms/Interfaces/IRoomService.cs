namespace TestPlatform.Services.Database.Rooms.Interfaces
{
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Database.Interfaces;

    public interface IRoomService : IBaseService<Room>
    {
        IQueryable<T> FindAllRoomsAsQueryable<T>(Guid userId);

        IQueryable<T> FindAllRoomsByUserIdAsQueryable<T>(Guid userId);

        Task<T> FindRoomByUserIdAndTestIdAsync<T>(Guid userId, Guid testId);

        Task UpdateParticipantsAsync(Guid roomId, IEnumerable<Guid> participantIds, Guid currentUserId);

        Task HardDeleteParticipantsAsync(Guid roomId, IEnumerable<Guid> participantIds);
    }
}
