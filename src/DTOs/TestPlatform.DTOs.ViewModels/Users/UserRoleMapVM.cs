namespace TestPlatform.DTOs.ViewModels.Users
{
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UserRoleMapVM : IMapFrom<UserRoleMap>
    {
        public string RoleName { get; set; }
    }
}
