namespace TestPlatform.Services.Database.Subjects
{
    using AutoMapper;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;

    public class UserSubjectTagMapService : BaseService<UserSubjectTagMap>, IUserSubjectTagMapService
    {
        public UserSubjectTagMapService(IBaseRepository<UserSubjectTagMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
