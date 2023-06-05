namespace TestPlatform.Database.Entities.Comments
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Entities.Tests;

    public class TestComment : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_THIRTEEN, MinimumLength = Validations.ONE)]
        public string Content { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Guid TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
