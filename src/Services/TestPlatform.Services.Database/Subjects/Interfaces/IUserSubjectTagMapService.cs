namespace TestPlatform.Services.Database.Subjects.Interfaces
{
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Database.Interfaces;

    public interface IUserSubjectTagMapService : IBaseService<UserSubjectTagMap>
    {
        Task<IEnumerable<T>> FindUserSubjectTagsAsync<T>(Guid userId);

        Task<IEnumerable<T>> FindUserSubjectTagsBySubjectTagIdAsync<T>(Guid subjectTagId);

        Task UpdateUserSubjectTagsAsync(Guid userId, IEnumerable<Guid> userSubjectTags, Guid currentUserId);

        Task AddSubjectTagToUserAsync(Guid userId, Guid subjectTagIs, Guid currentUserId);

        Task RemoveSubjectTagFromUserAsync(Guid userSubjectTagId);
    }
}
