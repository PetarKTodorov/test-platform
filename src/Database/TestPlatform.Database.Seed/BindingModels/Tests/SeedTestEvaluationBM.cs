namespace TestPlatform.Database.Seed.BindingModels.Tests
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedTestEvaluationBM : IMapTo<TestEvaluation>
    {
        public Guid Id { get; set; }

        public int IsRounded { get; set; }

        public Guid TestId { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
