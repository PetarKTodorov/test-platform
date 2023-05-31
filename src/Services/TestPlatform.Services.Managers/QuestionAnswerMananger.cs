namespace TestPlatform.Services.Managers
{
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.DTOs.ViewModels.Questions;
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

        public async Task<T> CreateQuestion<T>(CreateQuestionBM model, Guid currentUserId)
        {
            var createdQuestion = await this.questionService.FindOrCreateAsync<BaseBM, CreateQuestionBM>(model, model.Title, currentUserId);

            var questionCopy = new CreateQuestionCopyBM()
            {
                OriginalQuestionId = createdQuestion.Id,
                HasRandomizedAnswers = model.HasRandomizedAnswers,
                SubjectTagId = model.SubjectTagId.Value,
                QuestionTypeId = model.QuestionTypeId.Value,
            };
            var createdQuestionCopy = await this.questionCopyService.CreateAsync<T, CreateQuestionCopyBM>(questionCopy, currentUserId);

            return createdQuestionCopy;
        }

        public async Task<T> UpdateQuestionAsync<T>(UpdateQuestionBM model, Guid currentUserId)
        {
            var question = await this.questionService.FindByIdAsync<QuestionVM>(model.OriginalQuestionId);
            if (question.Title != model.OriginalQuestionTitle)
            {
                question = await this.questionService.FindOrCreateAsync<QuestionVM, UpdateQuestionBM>(model, model.OriginalQuestionTitle, currentUserId);
            }
            model.OriginalQuestionId = question.Id;
            model.CreatedBy = currentUserId;

            var questionCopy = await this.questionCopyService.UpdateAsync<T, UpdateQuestionBM>(model.Id, model, currentUserId);

            return questionCopy;
        }

        public async Task AddAnswersToQuestionAsync(IEnumerable<UpdateQuestionAnswerBM> answers, Guid questionId, Guid currentUserId)
        {
            await this.questionAnswerMapService.HardDeleteAnswers(questionId);

            foreach (var answer in answers)
            {
                var createdAnswer = await this.answerService.FindOrCreateAsync<BaseBM>(answer.AnswerContent, currentUserId);

                var questionAnswerMap = new CreateQuestionAnswerBM()
                {
                    QuestionId = questionId,
                    AnswerId = createdAnswer.Id,
                    IsCorrect = answer.IsCorrect,
                };
                await this.questionAnswerMapService.CreateAsync<BaseBM, CreateQuestionAnswerBM>(questionAnswerMap, currentUserId);
            }
        }

        public async Task DeleteQuestionWithAnswersAsync(Guid questionId)
        {
            await this.questionAnswerMapService.HardDeleteAnswers(questionId);
            await this.questionCopyService.HardDeleteAsync<BaseBM>(questionId);
        }
    }
}
