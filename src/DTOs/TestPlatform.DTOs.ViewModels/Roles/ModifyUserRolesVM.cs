namespace TestPlatform.DTOs.ViewModels.Roles
{
    using System.Collections.Generic;
    using TestPlatform.DTOs.ViewModels.Users;

    public class ModifyUserRolesVM
    {
        public IEnumerable<UserRoleMapVM> UserRoles { get; set; }

        public IEnumerable<RoleVM> AllRoles { get; set; }
    }
}
