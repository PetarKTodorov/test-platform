namespace TestPlatform.Application.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("Error")]
    public class ErrorController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        [Route("404")]
        public IActionResult NotFound()
        {
            return this.View();
        }
    }
}
