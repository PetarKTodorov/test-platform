namespace TestPlatform.Services.Database.Questions.Interfaces
{
    using System;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Database.Interfaces;

    public interface IQuestionService : IBaseService<Question>
    {
        Task<T> FindOrCreateQuestionAsync<T, TModel>(TModel model, string title, Guid currentUserId);

        Task<T> FindQuestionByTitleAsync<T>(string title);
    }
}
