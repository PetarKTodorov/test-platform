﻿namespace TestPlatform.Services.Database.Questions
{
    using System.Collections.Generic;
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Mapper;

    public class QuestionCopyService : BaseService<QuestionCopy>, IQuestionCopyService
    {
        public QuestionCopyService(IBaseRepository<QuestionCopy> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }

        public IQueryable<T> FindUserQuestionsAsQueryable<T>(Guid userId)
        {
            var userQuestions = this.FindAllAsQueryable<BaseBM>()
                .Where(q => q.CreatedBy == userId)
                .To<T>();

            return userQuestions;
        }

        public IQueryable<T> FindUserQuestionsForTestAsQueryable<T>(Guid userId, IEnumerable<Guid> subjectTagsId, IEnumerable<Guid> testQuestionsIds)
        {
            var userQuestions = this.FindAllAsQueryable<QuestionCopyBM>()
                .Where(q => q.CreatedBy == userId)
                .Where(q => subjectTagsId.Contains(q.SubjectTagId))
                .Where(q => !testQuestionsIds.Contains(q.Id))
                .To<T>();

            return userQuestions;
        }
    }
}
