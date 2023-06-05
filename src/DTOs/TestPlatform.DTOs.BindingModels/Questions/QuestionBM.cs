namespace TestPlatform.DTOs.BindingModels.Questions
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class QuestionBM : IMapFrom<Question>
    {
        public string Title { get; set; }
    }
}
