namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System.ComponentModel;

    using AutoMapper;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsTestVM : IMapFrom<Test>, IHaveCustomMappings
    {
        public DetailsTestVM()
        {
            this.SubjectTagNames = new HashSet<string>();
        }

        public Guid Id { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Modified By")]
        public Guid? ModifiedBy { get; set; }

        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [DisplayName("Deleted By")]
        public Guid? DeletedBy { get; set; }

        [DisplayName("Deleted Date")]
        public DateTime? DeletedDate { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        public string Title { get; set; }

        public string Instructions { get; set; }

        [DisplayName("Is Approved")]
        public bool IsApproved { get; set; }

        [DisplayName("Status")]
        public string StatusName { get; set; }

        [DisplayName("Has Randomize Questions")]
        public bool HasRandomizeQuestions { get; set; }

        [DisplayName("Subject Tags")]
        public IEnumerable<string> SubjectTagNames { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Test, DetailsTestVM>()
                .ForMember(ctbm => ctbm.SubjectTagNames,
                    mo => mo.MapFrom(t => t.SubjectTags.Select(tsm => tsm.SubjectTag.Name)));
        }
    }
}
