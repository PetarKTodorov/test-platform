namespace TestPlatform.Services.Database.Questions.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Database.Interfaces;

    public interface IQuestionAnswerMapService : IBaseService<QuestionAnswerMap>
    {
        Task HardDeleteAnswers(Guid id);
    }
}
