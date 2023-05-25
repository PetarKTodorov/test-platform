namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.DTOs.BindingModels.Subjects;
    using TestPlatform.DTOs.ViewModels.Subjects;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class SubjectTagController : BaseAdministratorController
    {
        private readonly ISubjectTagService subjectTagService;
        private readonly ISearchPageableMananager searchPageableMananager;

        public SubjectTagController(ISubjectTagService subjectTagService,
            ISearchPageableMananager searchPageableMananager)
        {
            this.subjectTagService = subjectTagService;
            this.searchPageableMananager = searchPageableMananager;
        }

        [HttpGet]
        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.subjectTagService
                .FindAllAsQueryable<ListSubjectTagsVM>();

            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubjectTagBM model)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            await this.subjectTagService.CreateAsync<SubjectTag, CreateSubjectTagBM>(model, currentUserId);

            return this.RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, bool isDeleted = false)
        {
            var subjectTag = await this.subjectTagService.FindByIdAsync<DetailsSubjectTagVM>(id, isDeleted);

            return this.View(subjectTag);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var subjectTag = await this.subjectTagService.FindByIdAsync<UpdateSubjectTagBM>(id);

            return this.View(subjectTag);
        }

        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateSubjectTagBM model)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            await this.subjectTagService.UpdateAsync<SubjectTag, UpdateSubjectTagBM>(model.Id, model, currentUserId);

            return this.RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var subjectTag = await this.subjectTagService.FindByIdAsync<UpdateSubjectTagBM>(id);

            return this.View(subjectTag);
        }

        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateSubjectTagBM model)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            await this.subjectTagService.DeleteAsync<UpdateSubjectTagBM>(model.Id, currentUserId);

            return this.RedirectToAction("Details", new { id = model.Id, isDeleted = true });
        }

        [HttpGet]
        public async Task<IActionResult> Restore(Guid id)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            var subjectTag = await this.subjectTagService.RestoryAsync<DetailsSubjectTagVM>(id, currentUserId);

            return this.RedirectToAction("Details", new { id = subjectTag.Id });
        }
    }
}
