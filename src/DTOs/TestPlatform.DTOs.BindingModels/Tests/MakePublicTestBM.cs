namespace TestPlatform.DTOs.BindingModels.Tests
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Mapper.Interfaces;

    public class MakePublicTestBM : BaseBM, IMapFrom<Test>, IMapTo<Test>
    {
        public Guid StatusId { get; set; }
    }
}
