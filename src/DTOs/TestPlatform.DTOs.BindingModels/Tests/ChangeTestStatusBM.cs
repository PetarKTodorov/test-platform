namespace TestPlatform.DTOs.BindingModels.Tests
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ChangeTestStatusBM : BaseBM, IMapFrom<Test>, IMapTo<Test>, IHaveCustomMappings
    {
        [Required]
        [DisplayName("Status")]
        public Guid StatusId { get; set; }

        public IEnumerable<Guid> TestApprovalsIds { get; set; }

        public int TotalPoints { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Test, ChangeTestStatusBM>()
                .ForMember(dest => dest.TestApprovalsIds, mo => mo.MapFrom(src => src.Approvers.Select(a => a.Id)))
                .ForMember(dest => dest.TotalPoints, mo => mo.MapFrom(src => src.Questions.Sum(a => a.Points)));
        }
    }
}
