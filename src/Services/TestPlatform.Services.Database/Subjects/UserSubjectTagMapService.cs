namespace TestPlatform.Services.Database.Subjects
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Mapper;

    public class UserSubjectTagMapService : BaseService<UserSubjectTagMap>, IUserSubjectTagMapService
    {
        public UserSubjectTagMapService(IBaseRepository<UserSubjectTagMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }

        public async Task<IEnumerable<T>> FindUserSubjectTagsAsync<T>(Guid userId)
        {
            var userSubjectTags = await this.FindAllAsQueryable()
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToArrayAsync();

            return userSubjectTags;
        }

        public async Task<IEnumerable<T>> FindUserSubjectTagsBySubjectTagIdAsync<T>(Guid subjectTagId)
        {
            var userSubjectTags = await this.FindAllAsQueryable()
                .Where(x => x.SubjectTagId == subjectTagId)
                .To<T>()
                .ToArrayAsync();

            return userSubjectTags;
        }

        public async Task UpdateUserSubjectTagsAsync(Guid userId, IEnumerable<Guid> userSubjectTags, Guid currentUserId)
        {
            await this.AddSubjectTagsToUserAsync(userId, userSubjectTags, currentUserId);
            await this.RemoveSubjectTagsFromUserAsync(userId, userSubjectTags);
        }

        public async Task RemoveSubjectTagFromUserAsync(Guid userSubjectTagId)
        {
            await this.HardDeleteAsync<UserSubjectTagMap>(userSubjectTagId);
        }

        public async Task AddSubjectTagToUserAsync(Guid userId, Guid subjectTagIs, Guid currentUserId)
        {
            var userRoleMap = new UserSubjectTagMap()
            {
                UserId = userId,
                SubjectTagId = subjectTagIs,
            };

            await this.CreateAsync<UserSubjectTagMap, UserSubjectTagMap>(userRoleMap, currentUserId);
        }

        private async Task AddSubjectTagsToUserAsync(Guid userId, IEnumerable<Guid> newSubjectTags, Guid currentUserId)
        {
            var userSubjectTags = await this.FindUserSubjectTagsAsync<UserSubjectTagMap>(userId);
            var oldUserSubjectTagsIds = userSubjectTags.Select(st => st.SubjectTagId);

            var subjectTagsToAdd = newSubjectTags.Except(oldUserSubjectTagsIds);
            foreach (var subjectTagId in subjectTagsToAdd)
            {
                await this.AddSubjectTagToUserAsync(userId, subjectTagId, currentUserId);
            }
        }

        private async Task RemoveSubjectTagsFromUserAsync(Guid userId, IEnumerable<Guid> newSubjectTags)
        {
            var userSubjectTags = await this.FindUserSubjectTagsAsync<UserSubjectTagMap>(userId);
            var oldSubjectTagsIds = userSubjectTags.Select(st => st.SubjectTagId);

            var subjectTagsToRemove = oldSubjectTagsIds.Except(newSubjectTags);
            foreach (var subjectTagId in subjectTagsToRemove)
            {
                var subjectTag = userSubjectTags.First(ust => ust.SubjectTagId == subjectTagId);

                await this.RemoveSubjectTagFromUserAsync(subjectTag.Id);
            }
        }
    }
}
