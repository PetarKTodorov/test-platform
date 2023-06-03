namespace TestPlatform.Application.Areas.Student.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Constants;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.DTOs.BindingModels.Tests;
    using TestPlatform.DTOs.ViewModels.Tests;
    using TestPlatform.DTOs.ViewModels.Tests.ConductTest;
    using TestPlatform.DTOs.ViewModels.Tests.ConductTest.Valid;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class TestsController : BaseStudentController
    {
        private readonly IRoomService roomService;
        private readonly ITestService testService;
        private readonly ITestUserMapService testUserMapService;
        private readonly ISearchPageableMananager searchPageableMananager;

        public TestsController(IRoomService roomService,
            ITestService testService,
            ITestUserMapService testUserMapService,
            ISearchPageableMananager searchPageableMananager)
        {
            this.roomService = roomService;
            this.testService = testService;
            this.testUserMapService = testUserMapService;
            this.searchPageableMananager = searchPageableMananager;
        }

        [HttpGet]
        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.testUserMapService.FindByUserIdAsQueryable<ListStudentTestVM>(this.CurrentUserId);

            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> StartTest(Guid roomId)
        {
            var room = await this.roomService.FindByIdAsync<ConductTestVM>(roomId);

            if (room == null || !(room.StartDateTime <= DateTime.Now && DateTime.Now <= room.EndDateTime))
            {
                return this.NotFound();
            }

            var testUserMap = await this.testUserMapService.FindByTestIdAndUserIdAsync<CreateTestUserMapBM>(room.TestId, this.CurrentUserId);

            if (testUserMap != null && testUserMap.Grade != GlobalConstants.DEFAULT_TEST_GRADE_VALUE)
            {
                return this.RedirectToAction(nameof(List));
            }

            if (testUserMap == null)
            {
                var newTestUserMap = new CreateTestUserMapBM()
                {
                    TestId = room.TestId,
                    UserId = this.CurrentUserId,
                    Grade = GlobalConstants.DEFAULT_TEST_GRADE_VALUE
                };

                await this.testUserMapService.CreateAsync<BaseBM, CreateTestUserMapBM>(newTestUserMap, this.CurrentUserId);
            }

            return this.View(room);
        }

        [HttpPost]
        public async Task<IActionResult> FinishTest(ConductTestVM model)
        {
            var validTest = await this.testService.FindByIdAsync<ValidConductTestVM>(model.TestId);
            var totalPoints = validTest.Questions.Sum(q => q.Points);

            var studentPoints = 0;

            foreach (var question in model.TestQuestions)
            {
                var validQuestion = validTest.Questions.SingleOrDefault(q => q.QuestionId == question.QuestionId);
                var validAnswers = validQuestion.ValidAnswers;
                var submitedAnswers = question.SelectedAnswerIds == null ? new List<Guid>() : question.SelectedAnswerIds;

                bool hasSameGuids = validAnswers.OrderBy(g => g).SequenceEqual(submitedAnswers.OrderBy(g => g));

                if (hasSameGuids)
                {
                    studentPoints += validQuestion.Points;
                }
            }

            var grade = validTest.Evaluation.GradeScales.SingleOrDefault(gc => gc.GradeScaleLowerBound <= studentPoints && studentPoints <= gc.GradeScaleUpperBound).GradeScaleGrade;

            var testUserMap = await this.testUserMapService.FindByTestIdAndUserIdAsync<CreateTestUserMapBM>(model.TestId, this.CurrentUserId);

            testUserMap.Grade = grade;

            await this.testUserMapService.UpdateAsync<BaseBM, CreateTestUserMapBM>(testUserMap.Id, testUserMap, this.CurrentUserId);

            return this.RedirectToAction(nameof(List));
        }
    }
}
