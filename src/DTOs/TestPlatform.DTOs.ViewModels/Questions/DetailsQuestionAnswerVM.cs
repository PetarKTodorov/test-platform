namespace TestPlatform.DTOs.ViewModels.Questions
{
    using System;

    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsQuestionAnswerVM : IMapFrom<QuestionAnswerMap>
    {
        public Guid AnswerId { get; set; }

        public string AnswerContent { get; set; }

        public bool IsCorrect { get; set; }
    }
}
