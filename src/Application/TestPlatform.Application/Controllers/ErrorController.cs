namespace TestPlatform.Application.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult NotFound()
        {
            return this.View();
        }
    }
}
