namespace TestPlatform.Services.Database.Questions
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Mapper;

    public class QuestionCopyService : BaseService<QuestionCopy>, IQuestionCopyService
    {
        public QuestionCopyService(IBaseRepository<QuestionCopy> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }

        public async Task<IQueryable<T>> FindUserQuestionsAsQueryable<T>(Guid userId)
        {
            var userQuestions = this.FindAllAsQueryable<QuestionCopy>()
                .Where(q => q.CreatedBy == userId)
                .To<T>();

            return userQuestions;
        }
    }
}
