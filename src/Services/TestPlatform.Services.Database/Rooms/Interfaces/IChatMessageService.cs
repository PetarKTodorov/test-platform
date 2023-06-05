namespace TestPlatform.Services.Database.Rooms.Interfaces
{
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Database.Interfaces;

    public interface IChatMessageService : IBaseService<ChatMessage>
    {
        Task<IEnumerable<T>> FindByRoomIdAsync<T>(Guid roomId);
    }
}
