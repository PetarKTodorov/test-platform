namespace TestPlatform.Services.Database.Rooms
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Mapper;

    public class ChatConnectionService : BaseService<ChatConnection>, IChatConnectionService
    {
        public ChatConnectionService(IBaseRepository<ChatConnection> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
