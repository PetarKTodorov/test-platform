﻿namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.DTOs.BindingModels.Tests;
    using TestPlatform.DTOs.ViewModels.Tests;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class TestController : BaseTeacherController
    {
        private readonly ITestService testService;
        private readonly IStatusService statusService;
        private readonly ISubjectTagService subjectTagService;
        private readonly ISearchPageableMananager searchPageableMananager;

        public TestController(ITestService testService, IStatusService statusService,
            ISubjectTagService subjectTagService,
            ISearchPageableMananager searchPageableMananager)
        {
            this.testService = testService;
            this.statusService = statusService;
            this.subjectTagService = subjectTagService;
            this.searchPageableMananager = searchPageableMananager;
        }

        [HttpGet]
        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.testService
                .FindAllAsQueryable<ListTestsVM>();

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

            var privateStatus = await this.statusService.FindByNameAsync<Status>(GlobalConstants.PRIVATE_STATUS);

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

        //[HttpGet]
        //public async Task<IActionResult> Details(Guid id, bool isDeleted = false)
        //{
        //    var subjectTag = await this.subjectTagService.FindByIdAsync<DetailsSubjectTagVM>(id, isDeleted);

        //    return this.View(subjectTag);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Update(Guid id)
        //{
        //    var subjectTag = await this.subjectTagService.FindByIdAsync<UpdateSubjectTagBM>(id);

        //    return this.View(subjectTag);
        //}

        //[ValidateModelState]
        //[HttpPost]
        //public async Task<IActionResult> Update(UpdateSubjectTagBM model)
        //{
        //    var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
        //    await this.subjectTagService.UpdateAsync<SubjectTag, UpdateSubjectTagBM>(model.Id, model, currentUserId);

        //    return this.RedirectToAction(nameof(Details), new { id = model.Id });
        //}

        //[HttpGet]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var subjectTag = await this.subjectTagService.FindByIdAsync<UpdateSubjectTagBM>(id);

        //    return this.View(subjectTag);
        //}

        //[ValidateModelState]
        //[HttpPost]
        //public async Task<IActionResult> Delete(UpdateSubjectTagBM model)
        //{
        //    var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
        //    await this.subjectTagService.DeleteAsync<UpdateSubjectTagBM>(model.Id, currentUserId);

        //    return this.RedirectToAction(nameof(Details), new { id = model.Id, isDeleted = true });
        //}

        //[HttpGet]
        //public async Task<IActionResult> Restore(Guid id)
        //{
        //    var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
        //    var subjectTag = await this.subjectTagService.RestoryAsync<DetailsSubjectTagVM>(id, currentUserId);

        //    return this.RedirectToAction(nameof(Details), new { id = subjectTag.Id });
        //}
    }
}
