namespace TestPlatform.DTOs.BindingModels.User
{
    using TestPlatform.Services.Mapper.Interfaces;
    using TestPlatform.Database.Entities.Authorization;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;

    public class CreateUserBM : IMapTo<User>
    {
        [Required]
        [EmailAddress]
        [RegularExpression(Validations.EMAIL_REGEX, ErrorMessage = "This is invalid email format.")]
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
        [DataType(DataType.Password)]
        [RegularExpression(Validations.PASSWORD_REGEX, ErrorMessage = "Password field must be 10 symbols and contains at least one: <ul><li>Capital letter</li></ul>")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
