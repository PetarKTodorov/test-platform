namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.DTOs.BindingModels.Questions;
    using TestPlatform.DTOs.ViewModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class QuestionsController : BaseTeacherController
    {
        private readonly IQuestionService questionService;
        private readonly ISearchPageableMananager searchPageableMananager;

        public QuestionsController(IQuestionService questionService,
            ISearchPageableMananager searchPageableMananager)
        {
            this.questionService = questionService;
            this.searchPageableMananager = searchPageableMananager;
        }

        public async Task<IActionResult> List(int? page = 1)
        {
            var dataQuery = this.questionService.FindAllAsQueryable<QuestionInformationVM>();
            var model = this.searchPageableMananager.CreatePageableResult(dataQuery, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionBM model)
        {
            var currentUserId = Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
            await this.questionService.CreateAsync<Question, CreateQuestionBM>(model, currentUserId);

            return this.RedirectToAction(nameof(List));
        }
    }
}
