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

        public Guid QuestionTypeId { get; set; }

        [DisplayName("Question Type")]
        public List<SelectListItem> QuestionTypes { get; set; }

        public Guid SubjectTagId { get; set; }

        [DisplayName("Subject Tags")]
        public List<SelectListItem> SubjectTags { get; set; }
    }
}
