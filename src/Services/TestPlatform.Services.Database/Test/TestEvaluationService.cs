namespace TestPlatform.Services.Database.Test
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class TestEvaluationService : BaseService<TestEvaluation>, ITestEvaluationService
    {
        public TestEvaluationService(IBaseRepository<TestEvaluation> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }
    }
}
