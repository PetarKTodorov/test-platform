namespace TestPlatform.Services.Database.Test
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Mapper;

    public class TestEvaluationService : BaseService<TestEvaluation>, ITestEvaluationService
    {
        public TestEvaluationService(IBaseRepository<TestEvaluation> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }

        public async Task<T> FindTestEvaluationByTestIdAsync<T>(Guid testId)
        {
            return await this.FindAllAsQueryable()
                .Where(te => te.TestId == testId)
                .To<T>()
                .SingleOrDefaultAsync();
        }
    }
}
