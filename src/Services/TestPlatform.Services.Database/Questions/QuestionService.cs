namespace TestPlatform.Services.Database.Questions
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;

    public class QuestionService : BaseService<Question>, IQuestionService
    {
        public QuestionService(IBaseRepository<Question> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
