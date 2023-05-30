namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Constants;
    using TestPlatform.Common.Enums;
    using TestPlatform.Common.Extensions;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.DTOs.ViewModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class QuestionsController : BaseTeacherController
    {
        private readonly IQuestionCopyService questionCopyService;
        private readonly IQuestionTypeService questionTypeService;
        private readonly IQuestionAnswerMapService questionAnswerMapService;
        private readonly ISubjectTagService subjectTagService;
        private readonly IQuestionAnswerMananger questionAnswerMananger;
        private readonly ISearchPageableMananager searchPageableMananager;

        public QuestionsController(
            IQuestionCopyService questionCopyService,
            IQuestionTypeService questionTypeService,
            ISubjectTagService subjectTagService,
            IQuestionAnswerMananger questionAnswerMananger,
            ISearchPageableMananager searchPageableMananager)
        {
            this.questionCopyService = questionCopyService;
            this.questionTypeService = questionTypeService;
            this.subjectTagService = subjectTagService;
            this.questionAnswerMananger = questionAnswerMananger;
            this.searchPageableMananager = searchPageableMananager;
        }

        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = await this.questionCopyService.FindUserQuestionsAsQueryable<QuestionInformationVM>(this.CurrentUserId);
            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            this.ViewData["SubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();
            this.ViewData["QuestionTypes"] = (await this.questionTypeService.FindAllAsync<SelectListItem>(false)).ToList();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionBM model)
        {
            if (!this.ValidateQuestionModel(model.QuestionTypeId, model.Answers))
            {
                this.ViewData["SubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();
                this.ViewData["QuestionTypes"] = (await this.questionTypeService.FindAllAsync<SelectListItem>(false)).ToList();

                return this.View(model);
            }

            var createdQuestion = await this.questionAnswerMananger.CreateQuestion<QuestionCopy>(model, this.CurrentUserId);

            await this.questionAnswerMananger.AddAnswersToQuestionAsync(model.Answers, createdQuestion.Id, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = createdQuestion.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var question = await this.questionCopyService.FindByIdAsync<DetailsQuestionCopyVM>(id);
            if (question.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            return this.View(question);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var question = await this.questionCopyService.FindByIdAsync<UpdateQuestionBM>(id);
            if (question.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            this.ViewData["SubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();
            this.ViewData["QuestionTypes"] = (await this.questionTypeService.FindAllAsync<SelectListItem>(false)).ToList();

            return this.View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateQuestionBM model)
        {
            if (!this.ValidateQuestionModel(model.QuestionTypeId, model.Answers))
            {
                this.ViewData["SubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();
                this.ViewData["QuestionTypes"] = (await this.questionTypeService.FindAllAsync<SelectListItem>(false)).ToList();

                return this.View(model);
            }

            var question = await this.questionCopyService.FindByIdAsync<QuestionCopy>(model.Id);
            if (question.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            var questionCopy = await this.questionAnswerMananger.UpdateQuestionAsync<QuestionCopy>(model, this.CurrentUserId);
            await this.questionAnswerMananger.AddAnswersToQuestionAsync(model.Answers, questionCopy.Id, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var question = await this.questionCopyService.FindByIdAsync<DetailsQuestionCopyVM>(id);
            if (question.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            return this.View(question);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            var question = await this.questionCopyService.FindByIdAsync<QuestionCopy>(id);
            if (question.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            await this.questionAnswerMananger.DeleteQuestionWithAnswersAsync(id);

            return this.RedirectToAction(nameof(List));
        }

        private bool ValidateQuestionModel(Guid? questionTypeId, IEnumerable<UpdateQuestionAnswerBM> answers)
        {
            var isModelStateValid = true;

            if (!this.ModelState.IsValid)
            {
                isModelStateValid = false;
            }
            else if (!answers.Any() || answers.Count() < Validations.REQUIRRED_NUMBER_OF_ANSWERS_FOR_QUESTION)
            {
                isModelStateValid = false;

                this.ViewBag.StatusError = ErrorMessages.ENTER_AT_LEAST_TWO_ANSWERS_ERROR_MESSAGE;
            }
            else if (questionTypeId.Value == QuestionTypes.SingleChoice.GetUid()
                && answers.Count(a => a.IsCorrect) != Validations.REQUIRED_CORRECT_ANSWERS_FOR_SIGNLE_CHOICES)
            {
                isModelStateValid = false;

                this.ViewBag.StatusError = ErrorMessages.SELECT_ONE_CORRECT_ANSWER_ERROR_MESSAGE;
            }
            else if (questionTypeId.Value == QuestionTypes.MultipleChoice.GetUid()
                && answers.Count(a => a.IsCorrect) < Validations.REQUIRED_CORRECT_ANSWERS_FOR_MULTIPLE_CHOICE)
            {
                isModelStateValid = false;

                this.ViewBag.StatusError = ErrorMessages.SELECT_AT_LEAST_TWO_CORRECT_ANSWERS_ERROR_MESSAGE;
            }

            return isModelStateValid;
        }
    }
}
