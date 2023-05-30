namespace TestPlatform.Services.Database.Questions
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Mapper;

    public class AnswerService : BaseService<Answer>, IAnswerService
    {
        public AnswerService(IBaseRepository<Answer> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }

        public async Task<T> FindOrCreateAsync<T>(string answerContent, Guid currentUserId)
        {
            var answer = await this.FindByContentAsync<T>(answerContent);

            if (answer == null)
            {
                var answerModel = new Answer()
                {
                    Content = answerContent,
                };
                answer = await this.CreateAsync<T, Answer>(answerModel, currentUserId);
            }

            return answer;
        }

        public async Task<T> FindByContentAsync<T>(string content)
        {
            var answer = await this.FindAllAsQueryable<Answer>()
                .Where(a => a.Content == content)
                .To<T>()
                .FirstOrDefaultAsync();

            return answer;
        }
    }
}
