namespace TestPlatform.Services.Managers.Interfaces
{
    using TestPlatform.DTOs.BindingModels.Questions;

    public interface IQuestionAnswerMananger
    {
        Task<T> CreateQuestion<T>(CreateQuestionBM model, Guid currentUserId);

        Task<T> UpdateQuestionAsync<T>(UpdateQuestionBM model, Guid currentUserId);

        Task AddAnswersToQuestionAsync(IEnumerable<UpdateQuestionAnswerBM> answers, Guid questionId, Guid currentUserId);
    }
}
