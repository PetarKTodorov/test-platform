namespace TestPlatform.Application.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Filtres;

    [CustomAllowAnonymous]
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
