namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DeleteQuestionAnswerMap : IMapTo<QuestionAnswerMap>, IMapFrom<QuestionAnswerMap>
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }
    }
}
