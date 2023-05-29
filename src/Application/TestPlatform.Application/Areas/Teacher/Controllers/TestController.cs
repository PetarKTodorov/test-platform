namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Constants;
    using TestPlatform.Common.Enums;
    using TestPlatform.Common.Extensions;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.DTOs.BindingModels.Tests;
    using TestPlatform.DTOs.ViewModels.Tests;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;
    using static System.Net.Mime.MediaTypeNames;

    public class TestController : BaseTeacherController
    {
        private readonly ITestService testService;
        private readonly IStatusService statusService;
        private readonly ISubjectTagService subjectTagService;
        private readonly ISearchPageableMananager searchPageableMananager;
        private readonly ITestApprovalMapService testApprovalMapService;

        public TestController(ITestService testService, IStatusService statusService,
            ISubjectTagService subjectTagService,
            ISearchPageableMananager searchPageableMananager,
            ITestApprovalMapService testApprovalMapService)
        {
            this.testService = testService;
            this.statusService = statusService;
            this.subjectTagService = subjectTagService;
            this.searchPageableMananager = searchPageableMananager;
            this.testApprovalMapService = testApprovalMapService;
        }

        [HttpGet]
        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            var dataQuery = this.testService
                .FindAllAsQueryable<ListTestsVM>()
                .Where(lt => lt.CreatedBy == currentUserId);

            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ListPending(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            var dataQuery = this.testService
                .FindAllAsQueryable<ListPendingTestVM>()
                .Where(lt => lt.CreatedBy != currentUserId)
                .Where(lt => lt.StatusId == StatusType.Pending.GetUid())
                .Where(lt => !lt.ApproversIds.Contains(currentUserId));

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

            var privateStatus = await this.statusService.FindByIdAsync<Status>(StatusType.Private.GetUid());

            if (privateStatus == null)
            {
                this.ViewBag.StatusError = "Sorry, the test creation is temporarily denied because of invalid status.";

                return this.View(model);
            }

            model.StatusId = privateStatus.Id;

            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            var test = await this.testService.CreateAsync<Test, CreateTestBM>(model, currentUserId);

            await this.testService.UpdateSubjectTagsAsync(test.Id, model.SubjectTagsIds, currentUserId);

            return this.RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, bool isDeleted = false)
        {
            var subjectTag = await this.testService.FindByIdAsync<DetailsTestVM>(id, isDeleted);

            return this.View(subjectTag);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            this.ViewData["AllSubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

            var test = await this.testService.FindByIdAsync<UpdateTestBM>(id);
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));

            if (test.CreatedBy != currentUserId)
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

            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            if (model.CreatedBy != currentUserId)
            {
                return this.NotFound();
            }

            await this.testService.UpdateAsync<Test, UpdateTestBM>(model.Id, model, currentUserId);
            await this.testService.UpdateSubjectTagsAsync(model.Id, model.SubjectTagsIds, currentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            this.ViewData["AllSubjectTags"] = (await this.subjectTagService.FindAllAsync<SelectListItem>()).ToList();

            var test = await this.testService.FindByIdAsync<UpdateTestBM>(id);
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));

            if (test.CreatedBy != currentUserId)
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

            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            if (model.CreatedBy != currentUserId)
            {
                return this.NotFound();
            }

            await this.testService.DeleteAsync<UpdateTestBM>(model.Id, currentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id, isDeleted = true });
        }

        [HttpGet]
        public async Task<IActionResult> Restore(Guid id)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            var test = await this.testService.RestoryAsync<Test>(id, currentUserId);

            if (test.CreatedBy != currentUserId)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(Details), new { id = test.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Approve(Guid id)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));

            var test = await this.testService.FindByIdAsync<Test>(id);

            var newTestApprovalMap = new TestApprovalMap()
            {
                TestId = id,
                UserId = currentUserId,
            };

            await this.testApprovalMapService.CreateAsync<TestApprovalMap, TestApprovalMap>(newTestApprovalMap, currentUserId);

            if (test.Approvers.Count == 3)
            {
                // TODO
            }

            return this.RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
