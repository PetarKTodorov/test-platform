namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateQuestionCopyBM : IMapTo<QuestionCopy>
    {
        [Required]
        public Guid OriginalQuestionId { get; set; }

        public bool HasRandomizedAnswers { get; set; }

        [Required]
        public Guid QuestionTypeId { get; set; }

        [Required]
        public Guid SubjectTagId { get; set; }
    }
}
