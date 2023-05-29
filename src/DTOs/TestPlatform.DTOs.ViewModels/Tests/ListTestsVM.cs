namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System.ComponentModel;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ListTestsVM : IMapFrom<Test>
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public bool IsDeleted { get; set; }

        [CustomSearchField]
        public string Title { get; set; }

        public string Instructions { get; set; }

        [CustomSearchField]
        [DisplayName("Is Approved")]
        public bool IsApproved { get; set; }

        [CustomSearchField]
        [DisplayName("Status")]
        public string StatusName { get; set; }
    }
}
