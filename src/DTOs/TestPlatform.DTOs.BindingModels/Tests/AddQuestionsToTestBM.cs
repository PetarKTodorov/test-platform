namespace TestPlatform.DTOs.BindingModels.Tests
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class AddQuestionToTestBM : IHaveCustomMappings
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        [DisplayName("Test Title")]
        public string Title { get; set; }

        [DisplayName("Test Status")]
        public string StatusName { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        [DisplayName("Question")]
        public Guid QuestionId { get; set; }

        [DisplayName("Points")]
        public int QuestionPoints { get; set; }

        public IEnumerable<Guid> SubjectTagsIds { get; set; }

        public IEnumerable<Guid> QuestionsIds { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Test, AddQuestionToTestBM>()
                .ForMember(dest => dest.SubjectTagsIds, mo => mo.MapFrom(src => src.SubjectTags.Select(st => st.SubjectTagId)))
                .ForMember(dest => dest.QuestionsIds, mo => mo.MapFrom(src => src.Questions.Select(st => st.QuestionId)));
        }
    }
}
