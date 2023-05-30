namespace TestPlatform.Services.Database.Questions
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;

    public class QuestionAnswerMapService : BaseService<QuestionAnswerMap>, IQuestionAnswerMapService
    {
        public QuestionAnswerMapService(IQuestionAnswerMapRepository questionAnswerMapRepository, IMapper mapper)
            : base(questionAnswerMapRepository, mapper)
        {
        }

        public async Task HardDeleteAnswers(Guid questionId)
        {
            var questionAnswers = this.FindAllAsQueryable<QuestionAnswerMap>()
                .Where(qam => qam.QuestionId == questionId)
                .ToList();

            foreach (var questionAnswerMap in questionAnswers)
            {
                await this.HardDeleteAsync<QuestionAnswerMap>(questionAnswerMap.Id);
            }
        }
    }
}
