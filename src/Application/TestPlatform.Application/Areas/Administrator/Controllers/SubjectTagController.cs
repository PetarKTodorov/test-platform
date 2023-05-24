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
            if (page != null && page < 1)
            {
                page = 1;
            }

            if (searchCriteria == null || searchCriteria.Count == 0)
            {
                searchCriteria = typeof(AllSubjectTagsVM).GetDefaultSearchCriteria();
            }

            var result = new PageableResult<AllSubjectTagsVM>();

            var subjectTags = this.subjectTagService
                .FindAllAsQueryable<AllSubjectTagsVM>()
                .ApplySearchCriteria(searchCriteria)
                .Skip(result.PageSize * (page.Value - 1))
                .Take(result.PageSize)
                .ToArray();
            var subjectTagsCount = await this.subjectTagService.GetCountOfAllAsyns();

            result.Results = subjectTags;
            result.AllResultsCount = subjectTagsCount;
            result.CurrentPage = page.Value;

            var viewModel = new SearchFilterVM<AllSubjectTagsVM>()
            {
                Data = result,
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
            try
            {
                var subjectTag = await this.subjectTagService.FindByIdAsync<DetailsSubjectTagVM>(id);

                return this.View(subjectTag);
            }
            catch (NotFoundException exception)
            {
                return new NotFoundResult();
            }
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
