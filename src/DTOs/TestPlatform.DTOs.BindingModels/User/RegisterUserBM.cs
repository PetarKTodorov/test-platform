namespace TestPlatform.DTOs.BindingModels.User
{
    using TestPlatform.Services.Mapper.Interfaces;
    using TestPlatform.Database.Entities.Authorization;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;

    public class RegisterUserBM : IMapTo<User>
    {
        [Required]
        [EmailAddress]
        [RegularExpression(Validations.EMAIL_REGEX, ErrorMessage = "This is invalid email format.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Middle name")]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(Validations.PASSWORD_REGEX, ErrorMessage = "The password field must be 10 symbols and contains at least two: upper case and lower case letters, digits and special symbols.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
