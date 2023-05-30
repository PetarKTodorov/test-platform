namespace TestPlatform.Database.Seed.BindingModels.Tests
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedStatusBM : IMapTo<Status>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Name { get; set; }
    }
}
