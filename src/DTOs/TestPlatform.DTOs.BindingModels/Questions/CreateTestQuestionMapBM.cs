namespace TestPlatform.DTOs.BindingModels.Questions
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateTestQuestionMapBM : IMapTo<QuestionTestMap>
    {
        public Guid QuestionId { get; set; }

        public Guid TestId { get; set; }

        public int Points { get; set; }
    }
}
