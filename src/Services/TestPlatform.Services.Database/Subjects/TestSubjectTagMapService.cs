namespace TestPlatform.Services.Database.Subjects
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Mapper;

    public class TestSubjectTagMapService : BaseService<TestSubjectTagMap>, ITestSubjectTagMapService
    {
        public TestSubjectTagMapService(IBaseRepository<TestSubjectTagMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {

        }

        public async Task<IEnumerable<T>> FindAllByTestIdAsync<T>(Guid testId)
        {
            var testSubjectTagMaps = await this.BaseRepository.GetAllAsQueryable()
                .Where(tsm => tsm.TestId == testId)
                .To<T>()
                .ToListAsync();

            return testSubjectTagMaps;
        }
    }
}
