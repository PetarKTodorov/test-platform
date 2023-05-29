namespace TestPlatform.Services.Database.Test
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class TestApprovalMapService : BaseService<TestApprovalMap>, ITestApprovalMapService
    {
        public TestApprovalMapService(IBaseRepository<TestApprovalMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }
    }
}
