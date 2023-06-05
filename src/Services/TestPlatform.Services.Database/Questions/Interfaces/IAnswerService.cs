namespace TestPlatform.Services.Database.Questions.Interfaces
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Database.Interfaces;

    public interface IAnswerService : IBaseService<Answer>
    {
        Task<T> FindOrCreateAsync<T>(string answerContent, Guid currentUserId);

        Task<T> FindByContentAsync<T>(string content);
    }
}
