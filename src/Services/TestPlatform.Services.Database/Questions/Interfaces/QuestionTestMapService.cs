namespace TestPlatform.Services.Database.Questions.Interfaces
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Database.Interfaces;

    public interface IQuestionTestMapService : IBaseService<QuestionTestMap>
    {
        Task<T> FindQuestionTestAsync<T>(Guid questionId, Guid testId);

        int FindSumOfQuestionPointsByTest(Guid testId);
    }
}
