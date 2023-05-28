namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System;
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateQuestionAnswerBM : IMapFrom<QuestionAnswerMap>, IMapTo<QuestionAnswerMap>, IHaveCustomMappings
    {
        public Guid? Id { get; set; }

        public Guid? AnswerId { get; set; }

        public string AnswerContent { get; set; }

        public bool IsCorrect { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UpdateQuestionAnswerBM, Answer>()
                .ForMember(a => a.Id, mo => mo.MapFrom(uqabm => uqabm.AnswerId))
                .ForMember(a => a.Content, mo => mo.MapFrom(uqabm => uqabm.AnswerContent));
        }
    }
}
