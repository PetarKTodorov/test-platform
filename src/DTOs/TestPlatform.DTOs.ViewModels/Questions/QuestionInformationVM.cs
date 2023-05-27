namespace TestPlatform.DTOs.ViewModels.Questions
{
    using TestPlatform.Services.Mapper.Interfaces;
    using TestPlatform.Database.Entities.Questions;

    public class QuestionInformationVM : IMapFrom<Question>
    {
        public string Title { get; set; }
    }
}
