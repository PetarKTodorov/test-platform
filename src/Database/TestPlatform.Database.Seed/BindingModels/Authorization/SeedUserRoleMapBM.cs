namespace TestPlatform.Database.Seed.BindingModels.Authorization
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedUserRoleMapBM : IMapTo<UserRoleMap>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}
