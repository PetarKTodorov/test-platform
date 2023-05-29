namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.DTOs.ViewModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class QuestionsController : BaseTeacherController
    {
        private readonly IQuestionService questionService;
        private readonly IQuestionCopyService questionCopyService;
        private readonly IQuestionTypeService questionTypeService;
        private readonly IAnswerService answerService;
        private readonly IQuestionAnswerMapService questionAnswerMapService;
        private readonly ISubjectTagService subjectTagService;
        private readonly IQuestionAnswerMananger questionAnswerMananger;
        private readonly ISearchPageableMananager searchPageableMananager;

        public QuestionsController(IQuestionService questionService,
            IQuestionCopyService questionCopyService,
            IQuestionTypeService questionTypeService,
            IAnswerService answerService,
            IQuestionAnswerMapService questionAnswerMapService,
            ISubjectTagService subjectTagService,
            IQuestionAnswerMananger questionAnswerMananger,
            ISearchPageableMananager searchPageableMananager)
        {
            this.questionService = questionService;
            this.questionCopyService = questionCopyService;
            this.questionTypeService = questionTypeService;
            this.answerService = answerService;
            this.questionAnswerMapService = questionAnswerMapService;
            this.subjectTagService = subjectTagService;
            this.questionAnswerMananger = questionAnswerMananger;
            this.searchPageableMananager = searchPageableMananager;
        }

        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.questionCopyService.FindAllAsQueryable<QuestionInformationVM>();
            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateQuestionBM()
            {
                QuestionTypes = (await this.questionTypeService.FindAllAsync<SelectListItem>(false)).ToList(),
                SubjectTags = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionBM model)
        {
            if (!this.ModelState.IsValid)
            {
                model.QuestionTypes = (await this.questionTypeService.FindAllAsync<SelectListItem>(false)).ToList();
                model.SubjectTags = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

                return this.View(model);
            }

            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));

            var createdQuestion = await this.questionService.FindOrCreateAsync<Question, CreateQuestionBM>(model, model.Title, currentUserId);
            var questionCopy = new CreateQuestionCopyBM()
            {
                OriginalQuestionId = createdQuestion.Id,
                HasRandomizedAnswers = model.HasRandomizedAnswers,
                SubjectTagId = model.SubjectTagId.Value,
                QuestionTypeId = model.QuestionTypeId.Value,
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
        public async Task<IActionResult> Update(Guid id)
        {
            var question = await this.questionCopyService.FindByIdAsync<UpdateQuestionBM>(id);

            question.QuestionTypes = (await this.questionTypeService.FindAllAsync<SelectListItem>(false)).ToList();
            question.SubjectTags = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

            return this.View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateQuestionBM model)
        {
            if (!this.ModelState.IsValid)
            {
                model.QuestionTypes = (await this.questionTypeService.FindAllAsync<SelectListItem>(false)).ToList();
                model.SubjectTags = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

                return this.View(model);
            }

            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            var questionCopy = await this.questionAnswerMananger.UpdateQuestionAsync<QuestionCopy>(model, currentUserId);
            await this.questionAnswerMananger.AddAnswersToQuestionAsync(model.Answers, questionCopy.Id, currentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var question = await this.questionCopyService.FindByIdAsync<DetailsQuestionCopyVM>(id);

            return this.View(question);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            await this.questionCopyService.HardDeleteAsync<QuestionCopy>(id);

            return this.RedirectToAction(nameof(List));
        }
    }
}
