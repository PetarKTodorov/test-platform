namespace TestPlatform.Services.Database.Test
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class GradeScaleTestЕvaluationMapService : BaseService<GradeScaleTestЕvaluationMap>, IGradeScaleTestЕvaluationMapService
    {
        public GradeScaleTestЕvaluationMapService(IBaseRepository<GradeScaleTestЕvaluationMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }
    }
}
