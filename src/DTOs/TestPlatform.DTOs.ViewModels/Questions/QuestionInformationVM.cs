namespace TestPlatform.DTOs.ViewModels.Questions
{
    using TestPlatform.Services.Mapper.Interfaces;
    using TestPlatform.Database.Entities.Questions;
    using System.ComponentModel;

    public class QuestionInformationVM : IMapFrom<QuestionCopy>
    {
        [DisplayName("Title")]
        public string OriginalQuestionTitle { get; set; }

        [DisplayName("Question Type")]
        public string QuestionTypeName { get; set; }

        [DisplayName("Subject Tag")]
        public string SubjectTagName { get; set; }
    }
}
