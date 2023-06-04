namespace TestPlatform.DTOs.ViewModels.Users
{
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UserEmailVM : IMapFrom<User>
    {
        public string Email { get; set; }
    }
}
