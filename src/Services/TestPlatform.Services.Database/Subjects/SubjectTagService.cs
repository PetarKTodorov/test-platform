namespace TestPlatform.Services.Database.Subjects
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;

    public class SubjectTagService : BaseService<SubjectTag>, ISubjectTagService
    {
        private readonly IUserSubjectTagMapService userSubjectTagMapService;

        public SubjectTagService(IBaseRepository<SubjectTag> baseRepository,
            IMapper mapper,
            IUserSubjectTagMapService userSubjectTagMapService)
            : base(baseRepository, mapper)
        {
            this.userSubjectTagMapService = userSubjectTagMapService;
        }

        public override async Task<T> DeleteAsync<T>(Guid id, Guid currentUserId)
        {
            var resultFromDelete = await base.DeleteAsync<T>(id, currentUserId);

            await this.HardDeleteUserSubjectTagsMapAsync(id);

            return resultFromDelete;
        }

        private async Task HardDeleteUserSubjectTagsMapAsync(Guid subjectTagId)
        {
            var userSubjectTags = this.userSubjectTagMapService.FindAllAsQueryable<UserSubjectTagMap>()
                .Where(ustm => ustm.SubjectTagId == subjectTagId)
                .ToArray();

            foreach (var userSubjectTag in userSubjectTags)
            {
                await this.userSubjectTagMapService.HardDeleteAsync<UserSubjectTagMap>(userSubjectTag.Id);
            }
        }
    }
}
