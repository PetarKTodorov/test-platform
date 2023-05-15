namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> ListAll(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }

            var result = new PageableResult<AllSubjectTagsVM>();
            var subjectTags = await this.subjectTagService.FindAllAsync<AllSubjectTagsVM>(page.Value, result.PageSize);
            var subjectTagsCount = await this.subjectTagService.GetCountOfAllAsyns();

            result.Results = subjectTags;
            result.AllResultsCount = subjectTagsCount;
            result.CurrentPage = page.Value;

            return this.View(result);
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
        public async Task<IActionResult> Details()
        {
            return this.View();
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
