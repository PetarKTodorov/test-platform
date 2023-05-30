namespace TestPlatform.Database.Seed.BindingModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedQuestionCopyBM : IMapTo<QuestionCopy>
    {
        [Required]
        public Guid Id { get; set; }

        public Guid OriginalQuestionId { get; set; }

        public Guid QuestionTypeId { get; set; }

        public Guid SubjectTagId { get; set; }

        [Required]
        public bool HasRandomizedAnswers { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
