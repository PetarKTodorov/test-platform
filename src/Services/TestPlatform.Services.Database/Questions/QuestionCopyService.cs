namespace TestPlatform.Services.Database.Questions
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;

    public class QuestionCopyService : BaseService<QuestionCopy>, IQuestionCopyService
    {
        public QuestionCopyService(IBaseRepository<QuestionCopy> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
