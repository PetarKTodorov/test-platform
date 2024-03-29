﻿namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Constants;
    using TestPlatform.Common.Enums;
    using TestPlatform.Common.Extensions;
    using TestPlatform.DTOs.BindingModels.Comments;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.DTOs.BindingModels.Tests;
    using TestPlatform.DTOs.ViewModels.Tests;
    using TestPlatform.DTOs.ViewModels.Users;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Database.Comments.Interfaces;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class TestController : BaseTeacherController
    {
        private readonly ITestService testService;
        private readonly IStatusService statusService;
        private readonly ISubjectTagService subjectTagService;
        private readonly ISearchPageableMananager searchPageableMananager;
        private readonly ITestApprovalMapService testApprovalMapService;
        private readonly IQuestionCopyService questionCopyService;
        private readonly IQuestionTestMapService questionTestMapService;
        private readonly ITestGradeScaleManager testGradeScaleManager;
        private readonly IRoomService roomService;
        private readonly IUserService userService;
        private readonly ITestCommentService testCommentService;

        public TestController(ITestService testService,
            IStatusService statusService,
            ISubjectTagService subjectTagService,
            ISearchPageableMananager searchPageableMananager,
            ITestApprovalMapService testApprovalMapService,
            IQuestionCopyService questionCopyService,
            IQuestionTestMapService questionTestMapService,
            ITestGradeScaleManager testGradeScaleManager,
            IRoomService roomService,
            IUserService userService,
            ITestCommentService testCommentService)
        {
            this.testService = testService;
            this.statusService = statusService;
            this.subjectTagService = subjectTagService;
            this.searchPageableMananager = searchPageableMananager;
            this.testApprovalMapService = testApprovalMapService;
            this.questionCopyService = questionCopyService;
            this.questionTestMapService = questionTestMapService;
            this.testGradeScaleManager = testGradeScaleManager;
            this.roomService = roomService;
            this.userService = userService;
            this.testCommentService = testCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.testService.FindUserTestsAsQueryable<ListTestsVM>(this.CurrentUserId);
            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ListPending(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.testService.FindPendingTestAsQueryable<ListPendingTestVM>(this.CurrentUserId)
                .Where(lt => !lt.ApproversIds.Contains(this.CurrentUserId));

            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            this.ViewData["AllSubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestBM model)
        {
            if (this.ModelState.IsValid == false)
            {
                this.ViewData["AllSubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

                return this.View(model);
            }

            var privateStatus = await this.statusService.FindByIdAsync<BaseBM>(StatusType.Private.GetUid());

            if (privateStatus == null)
            {
                this.ViewBag.StatusError = "Sorry, the test creation is temporarily denied because of invalid status.";

                return this.View(model);
            }

            model.StatusId = privateStatus.Id;

            var test = await this.testService.CreateAsync<BaseBM, CreateTestBM>(model, this.CurrentUserId);

            await this.testService.UpdateSubjectTagsAsync(test.Id, model.SubjectTagsIds, this.CurrentUserId);

            return this.RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, bool isDeleted = false)
        {
            var test = await this.testService.FindByIdAsync<DetailsTestVM>(id, isDeleted);
            test.CreatedByEmail = (await this.userService.FindByIdAsync<UserEmailVM>(test.CreatedBy)).Email;

            return this.View(test);
        }

        [HttpPost]
        public async Task<IActionResult> WriteComment(CreateCommentBM model)
        {
            if (this.ModelState.IsValid == false)
            {
                var test = await this.testService.FindByIdAsync<DetailsTestVM>(model.TestId, false);
                test.CreatedByEmail = (await this.userService.FindByIdAsync<UserEmailVM>(test.CreatedBy)).Email;

                test.CreateCommentBM = model;

                return this.View(nameof(Details), test);
            }

            await this.testCommentService.CreateAsync<BaseBM, CreateCommentBM>(model, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.TestId });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            this.ViewData["AllSubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

            var test = await this.testService.FindByIdAsync<UpdateTestBM>(id);

            if (test.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            return this.View(test);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTestBM model)
        {
            if (this.ModelState.IsValid == false)
            {
                this.ViewData["AllSubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

                return this.View(model);
            }

            if (model.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            await this.testService.UpdateAsync<BaseBM, UpdateTestBM>(model.Id, model, this.CurrentUserId);
            await this.testService.UpdateSubjectTagsAsync(model.Id, model.SubjectTagsIds, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            this.ViewData["AllSubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

            var test = await this.testService.FindByIdAsync<UpdateTestBM>(id);

            if (test.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            return this.View(test);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateTestBM model)
        {
            if (this.ModelState.IsValid == false)
            {
                this.ViewData["AllSubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

                return this.View(model);
            }

            if (model.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            await this.testService.DeleteAsync<UpdateTestBM>(model.Id, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id, isDeleted = true });
        }

        [HttpGet]
        public async Task<IActionResult> Restore(Guid id)
        {
            var test = await this.testService.RestoryAsync<BaseBM>(id, this.CurrentUserId);

            if (test.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(Details), new { id = test.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Approve(Guid id)
        {
            var newTestApprovalMap = new CreateTestApprovalBM()
            {
                TestId = id,
                UserId = this.CurrentUserId,
            };

            await this.testApprovalMapService.CreateAsync<BaseBM, CreateTestApprovalBM>(newTestApprovalMap, this.CurrentUserId);

            var test = await this.testService.FindByIdAsync<UpdateApproveTestBM>(id);
            if (test.ApproversCount == GlobalConstants.DEFAULT_TEST_APPROVELS_COUNT)
            {
                test.IsApproved = true;
                test.StatusId = StatusType.Ready.GetUid();
                await this.testService.UpdateAsync<BaseBM, UpdateApproveTestBM>(test.Id, test, this.CurrentUserId);
            }

            return this.RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var test = await this.testService.FindByIdAsync<ChangeTestStatusBM>(id);
            if (test.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            this.ViewData["Statuses"] = this.testService.GetTestNextStatuses(test.StatusId);

            return this.View(test);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeTestStatusBM model)
        {
            var test = await this.testService.FindByIdAsync<ChangeTestStatusBM>(model.Id);
            if (test.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            if (!this.ValidateChangeStatus(test.Id, test.StatusId, model.StatusId))
            {
                this.ViewData["Statuses"] = this.testService.GetTestNextStatuses(test.StatusId);

                return this.View();
            }

            await this.testService.UpdateAsync<BaseBM, ChangeTestStatusBM>(model.Id, model, this.CurrentUserId);

            if (model.StatusId == StatusType.Private.GetUid())
            {
                foreach (var testApprovalId in test.TestApprovalsIds)
                {
                    await this.testApprovalMapService.HardDeleteAsync<BaseBM>(testApprovalId);
                }

                await this.testGradeScaleManager.DeleteGradeScalesAsync(test.Id, this.CurrentUserId);
            }
            else if (model.StatusId == StatusType.Pending.GetUid())
            {
                await this.testGradeScaleManager.CreateGradeScaleForTestAsync(test.Id, test.TotalPoints, this.CurrentUserId);
            }

            return this.RedirectToAction(nameof(Details), new { id = test.Id });
        }

        [HttpGet]
        public async Task<IActionResult> AddQuestion(Guid testId)
        {
            var test = await this.testService.FindByIdAsync<AddQuestionToTestBM>(testId);

            if (test.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            this.ViewData["UserQuestions"] = this.questionCopyService
                .FindUserQuestionsForTestAsQueryable<SelectListItem>(this.CurrentUserId, test.SubjectTagsIds, test.QuestionsIds)
                .ToList();

            return this.View(test);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionToTestBM model)
        {
            if (!this.ValidateTestQuestion(model.QuestionPoints))
            {
                this.ViewData["UserQuestions"] = this.questionCopyService
                    .FindUserQuestionsForTestAsQueryable<SelectListItem>(this.CurrentUserId, model.SubjectTagsIds, model.QuestionsIds)
                    .ToList();

                return this.View(model);
            }

            var test = await this.testService.FindByIdAsync<AddQuestionToTestBM>(model.Id);
            if (test.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            var createModel = new CreateTestQuestionMapBM()
            {
                QuestionId = model.QuestionId,
                TestId = model.Id,
                Points = model.QuestionPoints,
            };
            await this.questionTestMapService.CreateAsync<BaseBM, CreateTestQuestionMapBM>(createModel, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveQuestion(Guid questionId, Guid testId)
        {
            var test = await this.testService.FindByIdAsync<BaseBM>(testId);
            if (test.CreatedBy != this.CurrentUserId)
            {
                return this.NotFound();
            }

            var question = await this.questionCopyService.FindByIdAsync<BaseBM>(questionId);
            var questionTestMap = await this.questionTestMapService.FindQuestionTestAsync<BaseBM>(question.Id, test.Id);

            await this.questionTestMapService.HardDeleteAsync<BaseBM>(questionTestMap.Id);

            return this.RedirectToAction(nameof(Details), new { id = test.Id });
        }

        private bool ValidateTestQuestion(int points)
        {
            var isValid = this.ModelState.IsValid;

            if (points <= Validations.ZERO)
            {
                isValid = false;

                this.ViewBag.StatusError = ErrorMessages.QUESTION_POINTS_MUST_BE_GREATER_THAN_ZERO;
            }

            return isValid;
        }

        private bool ValidateChangeStatus(Guid testId, Guid oldTestStatusId, Guid newTestStatusId)
        {
            var isValid = this.ModelState.IsValid;

            if ((oldTestStatusId == StatusType.Public.GetUid() || oldTestStatusId == StatusType.Ready.GetUid()) &&
                (newTestStatusId == StatusType.Private.GetUid() || newTestStatusId == StatusType.Ready.GetUid()))
            {
                var countOfRoomsCreatedWithTest = this.roomService.CountOfRoomsInTheFutureByTestId(testId);
                if (countOfRoomsCreatedWithTest > 0)
                {
                    isValid = false;

                    this.ViewBag.StatusError = string.Format(
                        ErrorMessages.THERE_ARE_ROOMS_IN_THE_FUTURE_WITH_THIS_TEST,
                        countOfRoomsCreatedWithTest
                    );
                }
            }
            else if (newTestStatusId == StatusType.Pending.GetUid() && oldTestStatusId == StatusType.Private.GetUid())
            {
                var points = this.questionTestMapService.FindSumOfQuestionPointsByTest(testId);
                if (points < GlobalConstants.COUNT_OF_GRADES)
                {
                    isValid = false;

                    this.ViewBag.StatusError = string.Format(
                        ErrorMessages.TEST_DOESNT_HAVE_ENOUGH_QUESTION_POINTS,
                        GlobalConstants.COUNT_OF_GRADES
                    );
                }
            }

            return isValid;
        }
    }
}
