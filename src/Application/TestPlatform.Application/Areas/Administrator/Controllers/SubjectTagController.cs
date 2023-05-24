namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.Searcher;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Exceptions;
    using TestPlatform.DTOs.ViewModels.Common;
    using TestPlatform.DTOs.ViewModels.SubjectTags;
    using TestPlatform.Services.Database.Subjects.Interfaces;

    public class SubjectTagController : BaseAdministratorController
    {
        private readonly ISubjectTagService subjectTagService;

        public SubjectTagController(ISubjectTagService subjectTagService)
        {
            this.subjectTagService = subjectTagService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            if (searchCriteria == null || searchCriteria.Count == 0)
            {
                searchCriteria = typeof(AllSubjectTagsVM).GetDefaultSearchCriteria();
            }

            var filteredDataQuery = this.subjectTagService
                .FindAllAsQueryable<AllSubjectTagsVM>()
                .ApplySearchCriteria(searchCriteria);

            var subjectTagsCount = filteredDataQuery.Count();
            var paging = new Paging(page.Value, subjectTagsCount);

            var filteredData = filteredDataQuery
                .Skip(paging.PageSize * (paging.CurrentPage - 1))
                .Take(paging.PageSize)
                .ToArray();

            var pageableData = new PageableResult<AllSubjectTagsVM>(filteredData, paging);

            var viewModel = new SearchFilterVM<AllSubjectTagsVM>()
            {
                Data = pageableData,
                SearchCriteria = searchCriteria
            };

            return this.View(viewModel);
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
