namespace TestPlatform.DTOs.ViewModels.Questions
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsQuestionTestVM : IMapFrom<QuestionTestMap>
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public string QuestionOriginalQuestionTitle { get; set; }

        public Guid QuestionQuestionTypeId { get; set; }

        public string QuestionQuestionTypeName { get; set; }

        public string QuestionSubjectTagName { get; set; }

        public int Points { get; set; }

        public IList<Guid> SelectedAnswerIds { get; set; }

        public IList<DetailsQuestionAnswerVM> QuestionAnswers { get; set; }
    }
}
