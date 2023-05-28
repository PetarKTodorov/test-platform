﻿namespace TestPlatform.Services.Database.Questions
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;

    public class QuestionAnswerMapService : BaseService<QuestionAnswerMap>, IQuestionAnswerMapService
    {
        public QuestionAnswerMapService(IBaseRepository<QuestionAnswerMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }
    }
}
