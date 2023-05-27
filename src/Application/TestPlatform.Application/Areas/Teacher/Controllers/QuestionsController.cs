namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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
    }
}
