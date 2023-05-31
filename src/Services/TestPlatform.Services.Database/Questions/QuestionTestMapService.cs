namespace TestPlatform.Services.Database.Questions
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;

    public class QuestionTestMapService : BaseService<QuestionTestMap>, IQuestionTestMapService
    {
        public QuestionTestMapService(IBaseRepository<QuestionTestMap> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }

        public async Task<T> FindQuestionTestAsync<T>(Guid questionId, Guid testId)
        {
            var questionTestMap = await this.FindAllAsQueryable<QuestionTestMapBM>()
                .SingleOrDefaultAsync(qtm => qtm.QuestionId == questionId && qtm.TestId == testId);

            var mappedObject = this.Mapper.Map<T>(questionTestMap);

            return mappedObject;
        }
    }
}
