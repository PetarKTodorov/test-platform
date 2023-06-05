namespace TestPlatform.DTOs.ViewModels.Tests
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class TestTitleVM : IMapFrom<Test>
    {
        public string Title { get; set; }
    }
}
