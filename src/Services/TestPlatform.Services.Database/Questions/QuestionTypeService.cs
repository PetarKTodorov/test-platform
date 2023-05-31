namespace TestPlatform.Services.Database.Questions
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;

    public class QuestionTypeService : BaseService<QuestionType>, IQuestionTypeService
    {
        public QuestionTypeService(IBaseRepository<QuestionType> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
