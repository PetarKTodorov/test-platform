namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateQuestionBM : IMapTo<QuestionCopy>, IMapFrom<QuestionCopy>, IHaveCustomMappings
    {
        public UpdateQuestionBM()
        {
            this.Answers = new HashSet<UpdateQuestionAnswerBM>();
        }

        [Required]
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        [Required]
        public Guid OriginalQuestionId { get; set; }

        [Required]
        [DisplayName("Title")]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string OriginalQuestionTitle { get; set; }

        [Required]
        [DisplayName("Has Randomized Answers")]
        public bool HasRandomizedAnswers { get; set; }

        [DisplayName("Question Type")]
        public Guid QuestionTypeId { get; set; }

        [DisplayName("Subject Tag")]
        public Guid SubjectTagId { get; set; }

        public IEnumerable<UpdateQuestionAnswerBM> Answers { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UpdateQuestionBM, Question>()
                .ForMember(q => q.Title, mo => mo.MapFrom(uqbm => uqbm.OriginalQuestionTitle));

            configuration.CreateMap<UpdateQuestionBM, QuestionCopy>()
                .ForMember(uqbm => uqbm.Answers, mo => mo.Ignore());
        }
    }
}
