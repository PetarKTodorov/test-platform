namespace TestPlatform.Services.Database.Questions
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;

    public class AnswerService : BaseService<Answer>, IAnswerService
    {
        public AnswerService(IBaseRepository<Answer> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
