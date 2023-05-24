namespace TestPlatform.Services.Database.Subjects
{
    using AutoMapper;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;

    public class SubjectTagService : BaseService<SubjectTag>, ISubjectTagService
    {
        public SubjectTagService(IBaseRepository<SubjectTag> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }


    }
}
