namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateQuestionBM : IMapTo<QuestionCopy>, IMapFrom<QuestionCopy>, IHaveCustomMappings
    {
        public UpdateQuestionBM()
        {
            this.AnswersContent = new HashSet<string>();
        }

        [Required]
        public Guid Id { get; set; }

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

        public List<SelectListItem> QuestionTypes { get; set; }

        public List<SelectListItem> SubjectTags { get; set; }

        [DisplayName("Answers")]
        public IEnumerable<string> AnswersContent { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UpdateQuestionBM, Question>()
                .ForMember(q => q.Title, mo => mo.MapFrom(uqbm => uqbm.OriginalQuestionTitle));

            configuration.CreateMap<QuestionCopy, UpdateQuestionBM>()
                .ForMember(uqbm => uqbm.AnswersContent, mo => mo.MapFrom(qc => qc.Answers.Select(a => a.Answer.Content)));
        }
    }
}
