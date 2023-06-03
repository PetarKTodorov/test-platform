namespace TestPlatform.DTOs.BindingModels.Tests
{
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateTestUserMapBM : IMapTo<TestUserMap>, IMapFrom<TestUserMap>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TestId { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Grade { get; set; }
    }
}
