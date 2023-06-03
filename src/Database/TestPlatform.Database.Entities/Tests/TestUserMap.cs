namespace TestPlatform.Database.Entities.Tests
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Authorization;

    public class TestUserMap : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Grade { get; set; }
    }
}
