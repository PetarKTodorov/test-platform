namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateQuestionBM : IMapTo<Question>
    {
        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Has Randomized Answers")]
        public bool HasRandomizedAnswers { get; set; }

        [Required]
        [DisplayName("Question Type")]
        public Guid? QuestionTypeId { get; set; }

        public List<SelectListItem> QuestionTypes { get; set; }

        [Required]
        [DisplayName("Subject Tag")]
        public Guid? SubjectTagId { get; set; }

        public List<SelectListItem> SubjectTags { get; set; }
    }
}
