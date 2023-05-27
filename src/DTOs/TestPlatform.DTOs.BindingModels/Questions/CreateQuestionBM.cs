namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateQuestionBM : IMapTo<Question>
    {
        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Title { get; set; }

        [Required]
        public bool HasRandomizedAnswers { get; set; }

        public Guid QuestionTypeId { get; set; }

        public Guid SubjectTagId { get; set; }
    }
}
