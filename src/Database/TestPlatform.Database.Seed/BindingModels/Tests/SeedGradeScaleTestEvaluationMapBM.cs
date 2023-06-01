namespace TestPlatform.Database.Seed.BindingModels.Tests
{
    using System;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedGradeScaleTestEvaluationMapBM : IMapTo<GradeScaleTestEvaluationMap>
    {
        public Guid Id { get; set; }

        public Guid GradeScaleId { get; set; }

        public Guid TestEvaluationId { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
