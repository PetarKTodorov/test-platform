namespace TestPlatform.DTOs.BindingModels.Tests
{
    using System.ComponentModel.DataAnnotations;
 
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateTestEvaluationBM : IMapTo<TestEvaluation>
    {
        public bool IsRouned { get; set; }

        [Required]
        public Guid TestId { get; set; }
    }
}
