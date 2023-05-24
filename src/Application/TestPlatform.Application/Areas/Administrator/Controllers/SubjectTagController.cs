namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.DTOs.ViewModels.SubjectTags;
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
        public async Task<IActionResult> ListAll(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.subjectTagService
                .FindAllAsQueryable<AllSubjectTagsVM>();

            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int dumy)
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var subjectTag = await this.subjectTagService.FindByIdAsync<DetailsSubjectTagVM>(id);

            return this.View(subjectTag);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int dumy)
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int dumy)
        {
            return this.View();
        }
    }
}
