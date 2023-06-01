namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System;
    using System.Collections.Generic;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class TestEvaluationGradeScaleVM : IMapFrom<TestEvaluation>
    {
        public Guid Id { get; set; }

        public ICollection<GradeScaleTestЕvaluationMapVM> GradeScales { get; set; }
    }
}
