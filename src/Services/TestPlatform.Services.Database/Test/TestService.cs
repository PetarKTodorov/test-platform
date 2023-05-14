namespace TestPlatform.Services.Database.Test
{
    using AutoMapper;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class TestService : BaseService<Test>, ITestService
    {
        public TestService(IBaseRepository<Test> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
