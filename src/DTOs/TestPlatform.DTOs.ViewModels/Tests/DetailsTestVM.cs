namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System.ComponentModel;

    using AutoMapper;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.DTOs.BindingModels.Comments;
    using TestPlatform.DTOs.ViewModels.Questions;
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

        [DisplayName("Created By")]
        public Guid CreatedBy { get; set; }

        [DisplayName("Created By")]
        public string CreatedByEmail { get; set; }

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

        [DisplayName("Total points")]
        public int TotalPoints { get; set; }

        [DisplayName("Approvals count")]
        public int ApproversCount { get; set; }

        [DisplayName("Questions count")]
        public int QuestionsCount { get; set; }

        public DetailsTestEvaluationVM Evaluation { get; set; }

        public CreateCommentBM CreateCommentBM { get; set; }

        public IEnumerable<DetailsQuestionTestVM> Questions { get; set; }

        public IEnumerable<CreateCommentBM> CreatedComments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Test, DetailsTestVM>()
                .ForMember(ctbm => ctbm.SubjectTagNames,
                    mo => mo.MapFrom(t => t.SubjectTags.Select(tsm => tsm.SubjectTag.Name)))
                .ForMember(ctbm => ctbm.TotalPoints, mo => mo.MapFrom(t => t.Questions.Sum(q => q.Points)))
                .ForMember(ctbm => ctbm.CreatedComments, mo => mo.MapFrom(t => t.Comments.Where(x => x.TestId == t.Id)));
        }
    }
}
