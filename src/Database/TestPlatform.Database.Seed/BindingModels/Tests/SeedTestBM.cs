namespace TestPlatform.Database.Seed.BindingModels.Tests
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedTestBM : IMapTo<Test>
    {
        [Required]
        public Guid Id { get; set; }

        public Guid? CreatedBy { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Title { get; set; }

        [StringLength(maximumLength: Validations.TWO_POWER_SIXTEEN, MinimumLength = Validations.ONE)]
        public string Instructions { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        [Required]
        public bool HasRandomizeQuestions { get; set; }

        [Required]
        public Guid StatusId { get; set; }
    }
}
