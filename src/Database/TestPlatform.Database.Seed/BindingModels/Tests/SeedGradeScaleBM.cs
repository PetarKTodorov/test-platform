namespace TestPlatform.Database.Seed.BindingModels.Tests
{
    using System;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedGradeScaleBM : IMapTo<GradeScale>
    {
        public Guid Id { get; set; }

        public int LowerBound { get; set; }

        public int UpperBound { get; set; }

        public string Grade { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
