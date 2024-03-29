﻿namespace TestPlatform.Services.Database.Test.Interfaces
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Database.Interfaces;

    public interface ITestService : IBaseService<Test>
    {
        public Task UpdateSubjectTagsAsync(Guid testId, IEnumerable<Guid> subjectTagsIds, Guid currentUserId);

        IQueryable<T> FindUserTestsAsQueryable<T>(Guid userId);

        IQueryable<T> FindPendingTestAsQueryable<T>(Guid userId);

        List<SelectListItem> GetTestNextStatuses(Guid testStatusId);
    }
}
