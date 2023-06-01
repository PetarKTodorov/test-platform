namespace TestPlatform.DTOs.BindingModels.Tests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateGradeScaleTestEvaluationBM : IMapTo<GradeScaleTestЕvaluationMap>
    {
        [Required]
        public Guid GradeScaleId { get; set; }

        [Required]
        public Guid TestЕvaluationId { get; set; }
    }
}
