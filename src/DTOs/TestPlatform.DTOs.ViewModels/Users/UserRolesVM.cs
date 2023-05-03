namespace TestPlatform.DTOs.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UserRolesVM : IMapFrom<User>
    {
        public Guid Id { get; set; }

        public IEnumerable<UserRoleMapVM> Roles { get; set; }
    }
}
