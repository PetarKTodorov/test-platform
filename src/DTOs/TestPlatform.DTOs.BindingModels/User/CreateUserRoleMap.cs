namespace TestPlatform.DTOs.BindingModels.User
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateUserRoleMap : IMapTo<UserRoleMap>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}
