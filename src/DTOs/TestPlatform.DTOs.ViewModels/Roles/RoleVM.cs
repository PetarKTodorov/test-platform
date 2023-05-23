namespace TestPlatform.DTOs.ViewModels.Roles
{
    using System;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class RoleVM : IMapFrom<Role>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
