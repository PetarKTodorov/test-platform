namespace TestPlatform.Services.Managers
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class QuestionAnswerMananger : IQuestionAnswerMananger
    {
        private readonly IQuestionService questionService;
        private readonly IQuestionCopyService questionCopyService;
        private readonly IAnswerService answerService;
        private readonly IQuestionAnswerMapService questionAnswerMapService;

        public QuestionAnswerMananger(IQuestionService questionService,
            IQuestionCopyService questionCopyService,
            IAnswerService answerService,
            IQuestionAnswerMapService questionAnswerMapService)
        {
            this.questionService = questionService;
            this.questionCopyService = questionCopyService;
            this.answerService = answerService;
            this.questionAnswerMapService = questionAnswerMapService;
        }

        public async Task<T> UpdateQuestionAsync<T>(UpdateQuestionBM model, Guid currentUserId)
        {
            var question = await this.questionService.FindByIdAsync<Question>(model.OriginalQuestionId);
            if (question.Title != model.OriginalQuestionTitle)
            {
                question = await this.questionService.FindOrCreateAsync<Question, UpdateQuestionBM>(model, model.OriginalQuestionTitle, currentUserId);
            }
            model.OriginalQuestionId = question.Id;

            var questionCopy = await this.questionCopyService.UpdateAsync<T, UpdateQuestionBM>(model.Id, model, currentUserId);

            return questionCopy;
        }

        public async Task AddAnswersToQuestionAsync(IEnumerable<UpdateQuestionAnswerBM> answers, Guid questionId, Guid currentUserId)
        {
            await this.questionAnswerMapService.HardDeleteAnswers(questionId);

            foreach (var answer in answers)
            {
                var createdAnswer = await this.answerService.FindOrCreateAsync<Answer>(answer.AnswerContent, currentUserId);

                var questionAnswerMap = new QuestionAnswerMap()
                {
                    QuestionId = questionId,
                    AnswerId = createdAnswer.Id,
                    IsCorrect = answer.IsCorrect,
                };
                await this.questionAnswerMapService.CreateAsync<QuestionAnswerMap, QuestionAnswerMap>(questionAnswerMap, currentUserId);
            }
        }
    }
}
