namespace TestPlatform.Services.Database.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using TestPlatform.Common.Enums;
    using TestPlatform.Common.Extensions;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.DTOs.BindingModels.Subjects;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Mapper;

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
            var currentSubjectTags = await this.testSubjectTagMapService.FindAllByTestIdAsync<TestSubjectTagMap>(testId);
            var currentSubjectTagsIds = currentSubjectTags.Select(tsm => tsm.SubjectTagId);

            var subjectTagIdsToRemove = currentSubjectTagsIds.Except(subjectTagsIds);
            var subjectTagIdsToAdd = subjectTagsIds.Except(currentSubjectTagsIds);

            foreach (var subjectTagId in subjectTagIdsToRemove)
            {
                var subjectTagToRemove = currentSubjectTags
                    .SingleOrDefault(x => x.SubjectTagId == subjectTagId && x.TestId == testId);

                await this.testSubjectTagMapService.HardDeleteAsync<TestSubjectTagMap>(subjectTagToRemove.Id);
            }

            foreach (var subjectTagId in subjectTagIdsToAdd)
            {
                var newTestSubjectTagMap = new CreateTestSubjectTagMapBM()
                {
                    TestId = testId,
                    SubjectTagId = subjectTagId
                };

                await this.testSubjectTagMapService.CreateAsync<TestSubjectTagMap, CreateTestSubjectTagMapBM>(newTestSubjectTagMap, currentUserId);
            }
        }

        public IQueryable<T> FindUserTestsAsQueryable<T>(Guid userId)
        {
            return this
                .FindAllAsQueryable()
                .Where(lt => lt.CreatedBy == userId)
                .To<T>();
        }

        public IQueryable<T> FindPendingTestAsQueryable<T>(Guid userId)
        {
            return this
                .FindAllAsQueryable()
                .Where(lt => lt.CreatedBy != userId)
                .Where(lt => lt.StatusId == StatusType.Pending.GetUid())
                .To<T>();
        }

        public List<SelectListItem> GetTestNextStatuses(Guid testStatusId)
        {
            var statuses = new List<SelectListItem>();
            if (testStatusId == StatusType.Private.GetUid())
            {
                statuses.Add(new SelectListItem
                {
                    Text = StatusType.Pending.GetDisplayName(),
                    Value = StatusType.Pending.GetUid().ToString()
                });
            }
            else if (testStatusId == StatusType.Pending.GetUid())
            {
                statuses.Add(new SelectListItem
                {
                    Text = StatusType.Private.GetDisplayName(),
                    Value = StatusType.Private.GetUid().ToString()
                });
            }
            else if (testStatusId == StatusType.Ready.GetUid())
            {
                statuses.Add(new SelectListItem
                {
                    Text = StatusType.Public.GetDisplayName(),
                    Value = StatusType.Public.GetUid().ToString()
                });
                statuses.Add(new SelectListItem
                {
                    Text = StatusType.Private.GetDisplayName(),
                    Value = StatusType.Private.GetUid().ToString()
                });
            }
            else if (testStatusId == StatusType.Public.GetUid())
            {
                statuses.Add(new SelectListItem
                {
                    Text = StatusType.Ready.GetDisplayName(),
                    Value = StatusType.Ready.GetUid().ToString()
                });
                statuses.Add(new SelectListItem
                {
                    Text = StatusType.Private.GetDisplayName(),
                    Value = StatusType.Private.GetUid().ToString()
                });
            }

            return statuses;
        }
    }
}
