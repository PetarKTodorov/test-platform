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

    public class ChatMessageService : BaseService<ChatMessage>, IChatMessageService
    {
        public ChatMessageService(IBaseRepository<ChatMessage> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }

        public async Task<IEnumerable<T>> FindByRoomIdAsync<T>(Guid roomId)
        {
            return await this.FindAllAsQueryable()
                .Where(cm => cm.RoomId == roomId)
                .OrderBy(cm => cm.CreatedDate)
                .To<T>()
                .ToListAsync();
        }
    }
}
