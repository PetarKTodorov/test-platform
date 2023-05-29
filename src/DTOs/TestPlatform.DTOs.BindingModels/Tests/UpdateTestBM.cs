namespace TestPlatform.DTOs.BindingModels.Tests
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateTestBM : IMapTo<Test>, IMapFrom<Test>, IHaveCustomMappings
    {
        public UpdateTestBM()
        {
            this.SubjectTagsIds = new List<Guid>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Title { get; set; }

        [StringLength(maximumLength: Validations.TWO_POWER_SIXTEEN, MinimumLength = Validations.ONE)]
        public string Instructions { get; set; }

        [Required]
        [DisplayName("Has Randomize Questions")]
        public bool HasRandomizeQuestions { get; set; }

        [DisplayName("Subject Tags")]
        public IEnumerable<Guid> SubjectTagsIds { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Test, UpdateTestBM>()
                .ForMember(ctbm => ctbm.SubjectTagsIds, mo => mo.MapFrom(t => t.SubjectTags.Select(tsm => tsm.SubjectTagId)));
        }
    }
}
