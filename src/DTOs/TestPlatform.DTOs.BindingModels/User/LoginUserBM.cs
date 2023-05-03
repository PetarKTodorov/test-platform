namespace TestPlatform.DTOs.BindingModels.User
{
    using TestPlatform.Services.Mapper.Interfaces;
    using TestPlatform.Database.Entities.Authorization;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;

    public class LoginUserBM : IMapTo<User>
    {
        [Required]
        [EmailAddress]
        [RegularExpression(Validations.EMAIL_REGEX, ErrorMessage = "Invalid password or email.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(Validations.PASSWORD_REGEX, ErrorMessage = "Invalid password or email.")]
        public string Password { get; set; }
    }
}
