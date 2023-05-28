namespace TestPlatform.Services.Database.Test
{
    using AutoMapper;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class StatusService : BaseService<Status>, IStatusService
    {
        public StatusService(IBaseRepository<Status> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
