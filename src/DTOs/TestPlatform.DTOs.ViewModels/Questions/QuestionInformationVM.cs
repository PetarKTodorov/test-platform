namespace TestPlatform.DTOs.ViewModels.Questions
{
    using TestPlatform.Services.Mapper.Interfaces;
    using TestPlatform.Database.Entities.Questions;
    using System.ComponentModel;
    using TestPlatform.Application.Infrastructures.Filtres;

    public class QuestionInformationVM : IMapFrom<QuestionCopy>
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        [DisplayName("Title")]
        [CustomSearchField]
        public string OriginalQuestionTitle { get; set; }

        [DisplayName("Question Type")]
        [CustomSearchField]
        public string QuestionTypeName { get; set; }

        [DisplayName("Subject Tag")]
        [CustomSearchField]
        public string SubjectTagName { get; set; }
    }
}
