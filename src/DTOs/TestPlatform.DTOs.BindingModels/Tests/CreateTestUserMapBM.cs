namespace TestPlatform.DTOs.BindingModels.Tests
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateTestUserMapBM : IMapTo<TestUserMap>
    {
        public Guid UserId { get; set; }

        public Guid TestId { get; set; }
    }
}
