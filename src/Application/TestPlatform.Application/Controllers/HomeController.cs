namespace TestPlatform.Application.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return this.View();
        }
    }
}
