namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.DTOs.BindingModels.Subjects;
    using TestPlatform.DTOs.ViewModels.Questions;
    using TestPlatform.DTOs.ViewModels.Subjects;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class QuestionsController : BaseTeacherController
    {
        private readonly IQuestionService questionService;
        private readonly IQuestionCopyService questionCopyService;
        private readonly IQuestionTypeService questionTypeService;
        private readonly ISubjectTagService subjectTagService;
        private readonly ISearchPageableMananager searchPageableMananager;

        public QuestionsController(IQuestionService questionService,
            IQuestionCopyService questionCopyService,
            IQuestionTypeService questionTypeService,
            ISubjectTagService subjectTagService,
            ISearchPageableMananager searchPageableMananager)
        {
            this.questionService = questionService;
            this.questionCopyService = questionCopyService;
            this.questionTypeService = questionTypeService;
            this.subjectTagService = subjectTagService;
            this.searchPageableMananager = searchPageableMananager;
        }

        public async Task<IActionResult> List(int? page = 1)
        {
            var dataQuery = this.questionCopyService.FindAllAsQueryable<QuestionInformationVM>();
            var model = this.searchPageableMananager.CreatePageableResult(dataQuery, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateQuestionVM()
            {
                QuestionTypes = await this.questionTypeService.FindAllAsync<QuestionTypeVM>(false),
                SubjectTags = await this.subjectTagService.FindAllAsync<SubjectTagVm>(),
            };

            return this.View(model);
        }

        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionBM model)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));

            var createdQuestion = await this.questionService.FindOrCreateQuestionAsync<Question, CreateQuestionBM>(model, model.Title, currentUserId);
            var questionCopy = new CreateQuestionCopyBM()
            {
                OriginalQuestionId = createdQuestion.Id,
                HasRandomizedAnswers = model.HasRandomizedAnswers,
                SubjectTagId = model.SubjectTag,
                QuestionTypeId = model.QuestionType,
            };
            await this.questionCopyService.CreateAsync<QuestionCopy, CreateQuestionCopyBM>(questionCopy, currentUserId);

            return this.RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var question = await this.questionCopyService.FindByIdAsync<DetailsQuestionCopyVM>(id);

            return this.View(question);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var question = await this.questionCopyService.FindByIdAsync<DetailsQuestionCopyVM>(id);

            return this.View(question);
        }

        [ValidateModelState]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            await this.questionCopyService.HardDeleteAsync<QuestionCopy>(id);

            return this.RedirectToAction(nameof(List));
        }
    }
}
