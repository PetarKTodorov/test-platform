namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class GradeScaleTestЕvaluationMapVM : IMapFrom<GradeScaleTestЕvaluationMap>
    {
        public Guid Id { get; set; }

        public Guid GradeScaleId { get; set; }
    }
}
