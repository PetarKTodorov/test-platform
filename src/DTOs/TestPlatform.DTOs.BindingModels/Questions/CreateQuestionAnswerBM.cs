namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System;

    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateQuestionAnswerBM : IMapTo<QuestionAnswerMap>
    {
        public Guid QuestionId { get; set; }

        public Guid AnswerId { get; set; }

        public bool IsCorrect { get; set; }
    }
}
