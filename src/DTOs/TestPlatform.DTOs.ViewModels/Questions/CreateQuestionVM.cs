namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System.ComponentModel;
    using TestPlatform.DTOs.ViewModels.Questions;
    using TestPlatform.DTOs.ViewModels.Subjects;

    public class CreateQuestionVM
    {
        public string Title { get; set; }

        [DisplayName("Has Randomized Answers")]
        public bool HasRandomizedAnswers { get; set; }

        [DisplayName("Question Type")]
        public IEnumerable<QuestionTypeVM> QuestionTypes { get; set; }

        [DisplayName("Subject Tags")]
        public IEnumerable<SubjectTagVm> SubjectTags { get; set; }
    }
}
