namespace TestPlatform.DTOs.ViewModels.Users
{
    using System;
    using System.ComponentModel;
    using AutoMapper;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UserInformationVM : IMapFrom<User>
    {
        public string Id { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [DisplayName("Deleted On")]
        public DateTime DeletedOn { get; set; }

        public IEnumerable<UserRoleMapVM> Roles { get; set; }
    }
}
