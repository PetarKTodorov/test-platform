namespace TestPlatform.Services.Database.Questions.Interfaces
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Database.Interfaces;

    public interface IQuestionCopyService : IBaseService<QuestionCopy>
    {
        Task<IQueryable<T>> FindUserQuestionsAsQueryable<T>(Guid userId);
    }
}
