namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System.ComponentModel;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateQuestionVM
    {
        public string Title { get; set; }

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
