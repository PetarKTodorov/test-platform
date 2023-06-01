namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class GradeScaleTestEvaluationMapVM : IMapFrom<GradeScaleTestEvaluationMap>
    {
        public Guid Id { get; set; }

        public Guid GradeScaleId { get; set; }
    }
}
