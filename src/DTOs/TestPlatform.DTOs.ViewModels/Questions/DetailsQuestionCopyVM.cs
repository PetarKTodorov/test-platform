namespace TestPlatform.DTOs.ViewModels.Questions
{
    using System;
    using System.ComponentModel;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsQuestionCopyVM : IMapFrom<QuestionCopy>
    {
        public Guid Id { get; set; }

        public Guid OriginalQuestionId { get; set; }

        [DisplayName("Title")]
        public string OriginalQuestionTitle { get; set; }

        [DisplayName("Has Randomized Answers")]
        public bool HasRandomizedAnswers { get; set; }

        [DisplayName("Question Type")]
        public string QuestionTypeName { get; set; }

        [DisplayName("Subject Tag")]
        public string SubjectTagName { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedDate { get; set; }

        public IEnumerable<UpdateQuestionAnswerBM> Answers { get; set; }
    }
}
