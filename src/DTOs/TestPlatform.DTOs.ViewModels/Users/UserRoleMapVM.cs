namespace TestPlatform.DTOs.ViewModels.Users
{
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UserRoleMapVM : IMapFrom<UserRoleMap>
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
