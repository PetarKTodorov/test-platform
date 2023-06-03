namespace TestPlatform.DTOs.BindingModels.Questions
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateAnswerBM : IMapTo<Answer>
    {
        public string Content { get; set; }
    }
}
