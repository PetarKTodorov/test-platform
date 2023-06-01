namespace TestPlatform.DTOs.BindingModels.Tests
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateApproveTestBM : IMapFrom<Test>, IMapTo<Test>
    {
        public Guid Id { get; set; }

        public int ApproversCount { get; set; }

        public bool IsApproved { get; set; }

        public Guid StatusId { get; set; }
    }
}
