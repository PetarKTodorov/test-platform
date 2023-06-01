namespace TestPlatform.Services.Database.Test
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Mapper;

    public class TestUserMapService : BaseService<TestUserMap>, ITestUserMapService
    {
        public TestUserMapService(IBaseRepository<TestUserMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }

        public async Task<T> FindByTestIdAndRoomIdAsync<T>(Guid testId, Guid userId)
        {
            var testUserMap = await this.FindAllAsQueryable()
                .Where(tum => tum.TestId == testId && tum.UserId == userId)
                .To<T>()
                .SingleOrDefaultAsync();

            return testUserMap;
        }
    }
}
