namespace TestPlatform.Services.Database.Rooms.Interfaces
{
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Database.Interfaces;

    public interface IRoomService : IBaseService<Room>
    {
        IQueryable<T> FindAllRoomsAsQueryable<T>(Guid userId);

        public Task UpdateParticipantsAsync(Guid roomId, IEnumerable<Guid> participantIds, Guid currentUserId);
    }
}
