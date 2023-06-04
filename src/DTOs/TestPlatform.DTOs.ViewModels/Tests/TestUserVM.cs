namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System.ComponentModel;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class TestUserVM : IMapFrom<TestUserMap>
    {
        [DisplayName("User Email")]
        public string UserEmail { get; set; }

        public string Grade { get; set; }

        [DisplayName("Submitted Date")]
        public DateTime CreatedDate { get; set; }
    }
}
