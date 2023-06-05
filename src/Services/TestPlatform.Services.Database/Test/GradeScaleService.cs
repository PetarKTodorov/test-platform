namespace TestPlatform.Services.Database.Test
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class GradeScaleService : BaseService<GradeScale>, IGradeScaleService
    {
        public GradeScaleService(IBaseRepository<GradeScale> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }
    }
}
