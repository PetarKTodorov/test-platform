namespace TestPlatform.DTOs.ViewModels.Tests.ConductTest.Valid
{
    using System.Collections.Generic;
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ValidQuestionTestMapVM : IMapFrom<QuestionTestMap>, IHaveCustomMappings
    {
        public Guid QuestionId { get; set; }

        public int Points { get; set; }

        public IEnumerable<Guid> ValidAnswers { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<QuestionTestMap, ValidQuestionTestMapVM>()
                .ForMember(ctbm => ctbm.ValidAnswers, mo => mo.MapFrom(t => t.Question.Answers.Where(a => a.IsCorrect.Value).Select(a => a.AnswerId)));
        }
    }
}
