namespace TestPlatform.Database.Seed.BindingModels.Tests
{
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedTestApprovalMapBM : IMapTo<TestApprovalMap>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid TestId { get; set; }
    }
}
