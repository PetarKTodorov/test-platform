namespace TestPlatform.DTOs.BindingModels.Tests
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateTestApprovalBM : IMapTo<TestApprovalMap>
    {
        public Guid TestId { get; set; }

        public Guid UserId { get; set; }
    }
}
