namespace TestPlatform.Database.Seed.BindingModels.Questions
{
    using System;

    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedQuestionTestMapBM : IMapTo<QuestionTestMap>
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public Guid TestId { get; set; }

        public Guid? CreatedBy { get; set; }

        public int Points { get; set; }
    }
}
