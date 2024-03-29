﻿namespace TestPlatform.Services.Database.Subjects.Interfaces
{
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Database.Interfaces;

    public interface ITestSubjectTagMapService : IBaseService<TestSubjectTagMap>
    {
        Task<IEnumerable<T>> FindAllByTestIdAsync<T>(Guid testId);
    }
}
