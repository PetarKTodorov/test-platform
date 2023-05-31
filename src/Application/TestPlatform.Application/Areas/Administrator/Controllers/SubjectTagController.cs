namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.DTOs.BindingModels.Common;
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
            var dataQuery = this.subjectTagService.FindAllSubjectTagsAsQueryable<ListSubjectTagsVM>();
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
            await this.subjectTagService.CreateAsync<BaseBM, CreateSubjectTagBM>(model, this.CurrentUserId);

            return this.RedirectToAction(nameof(List));
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
            await this.subjectTagService.UpdateAsync<BaseBM, UpdateSubjectTagBM>(model.Id, model, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id });
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
            await this.subjectTagService.DeleteAsync<UpdateSubjectTagBM>(model.Id, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = model.Id, isDeleted = true });
        }

        [HttpGet]
        public async Task<IActionResult> Restore(Guid id)
        {
            var subjectTag = await this.subjectTagService.RestoryAsync<DetailsSubjectTagVM>(id, this.CurrentUserId);

            return this.RedirectToAction(nameof(Details), new { id = subjectTag.Id });
        }
    }
}
