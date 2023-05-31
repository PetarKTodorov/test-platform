namespace TestPlatform.Services.Database.Questions
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Mapper;

    public class QuestionService : BaseService<Question>, IQuestionService
    {
        public QuestionService(IBaseRepository<Question> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }

        public async Task<T> FindQuestionByTitleAsync<T>(string title)
        {
            var question = await this.FindAllAsQueryable()
                .Where(q => q.Title == title)
                .To<T>()
                .FirstOrDefaultAsync();

            return question;
        }

        public async Task<T> FindOrCreateAsync<T, TModel>(TModel model, string title, Guid currentUserId)
        {
            var questionEntity = await this.FindQuestionByTitleAsync<T>(title);
            var createdQuestion = questionEntity;
            if (questionEntity == null)
            {
                createdQuestion = await this.CreateAsync<T, TModel>(model, currentUserId);
            }

            return createdQuestion;
        }
    }
}
