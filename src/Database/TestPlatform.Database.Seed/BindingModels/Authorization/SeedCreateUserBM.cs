namespace TestPlatform.Database.Seed.BindingModels.Authorization
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedCreateUserBM : IMapTo<User>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(Validations.EMAIL_REGEX)]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(Validations.PASSWORD_REGEX)]
        public string Password { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }
}
