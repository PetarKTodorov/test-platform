namespace TestPlatform.Database.Seed.BindingModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedQuestionAnswersMapBM : IMapTo<QuestionAnswerMap>
    {
        [Required]
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public Guid AnswerId { get; set; }

        [Required]
        public bool? IsCorrect { get; set; }
    }
}
