namespace TestPlatform.Services.Database.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.DTOs.BindingModels.Subjects;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;

    public class TestService : BaseService<Test>, ITestService
    {
        private readonly ITestSubjectTagMapService testSubjectTagMapService;

        public TestService(IBaseRepository<Test> baseRepository, IMapper mapper,
            ITestSubjectTagMapService testSubjectTagMapService)
            : base(baseRepository, mapper)
        {
            this.testSubjectTagMapService = testSubjectTagMapService;
        }

        public async Task UpdateSubjectTagsAsync(Guid testId, IEnumerable<Guid> subjectTagsIds, Guid currentUserId)
        {
            //var currentSubjectTags = (await this.testSubjectTagMapService.FindAllByTestIdAsync<TestSubjectTagMap>(testId))
            //var subjectTagsToRemove = currentSubjectTags.Where(tsm => !subjectTagsIds.Contains(tsm.SubjectTagId));
            //var subjectTagsToAdd = subjectTagsIds.Where(i => currentSubjectTags.Contains());

            foreach (var subjectTagId in subjectTagsIds)
            {
                var newTestSubjectTagMap = new CreateTestSubjectTagMapBM()
                {
                    TestId = testId,
                    SubjectTagId = subjectTagId
                };

                await this.testSubjectTagMapService.CreateAsync<TestSubjectTagMap, CreateTestSubjectTagMapBM>(newTestSubjectTagMap, currentUserId);
            }
        }
    }
}
