namespace TestPlatform.Application.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
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
