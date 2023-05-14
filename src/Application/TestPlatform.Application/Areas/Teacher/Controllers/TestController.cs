namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Filtres;

    using TestPlatform.DTOs.BindingModels.Test;

    public class TestController : BaseTeacherController
    {
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [CustomAllowAnonymous]
        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestBM model)
        {
            return this.RedirectToAction(actionName: "Index", controllerName: "Home", new { area = "" });
        }
    }
}
