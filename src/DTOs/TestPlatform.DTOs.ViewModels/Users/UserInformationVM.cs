namespace TestPlatform.DTOs.ViewModels.Users
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UserInformationVM : IMapFrom<User>
    {
        public string Id { get; set; }

        [CustomSearchField]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [CustomSearchField]
        public string Email { get; set; }

        [CustomSearchField]
        [DisplayName("Created date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [CustomSearchField]
        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [DisplayName("Deleted date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DeletedDate { get; set; }

        public IEnumerable<UserRoleMapVM> Roles { get; set; }
    }
}
