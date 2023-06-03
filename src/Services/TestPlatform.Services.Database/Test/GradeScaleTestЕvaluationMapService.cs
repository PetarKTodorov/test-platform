namespace TestPlatform.Services.Database.Test
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class GradeScaleTestEvaluationMapService : BaseService<GradeScaleTestEvaluationMap>, IGradeScaleTestEvaluationMapService
    {
        public GradeScaleTestEvaluationMapService(IBaseRepository<GradeScaleTestEvaluationMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }
    }
}
