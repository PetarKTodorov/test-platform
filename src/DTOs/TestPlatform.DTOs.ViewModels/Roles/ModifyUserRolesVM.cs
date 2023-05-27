namespace TestPlatform.DTOs.ViewModels.Roles
{
    using System.Collections.Generic;
    using TestPlatform.DTOs.ViewModels.Subjects;
    using TestPlatform.DTOs.ViewModels.Users;

    public class UpdateUserVM
    {
        public IEnumerable<UserRoleMapVM> UserRoles { get; set; }

        public IEnumerable<RoleVM> AllRoles { get; set; }

        public IEnumerable<UpdateUserSubjectTagMapVM> UserSubjectTags { get; set; }

        public IEnumerable<SubjectTagVm> AllSubjectTags { get; set; }
    }
}
