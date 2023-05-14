namespace TestPlatform.Application.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.ApplicationUser;

    public class BaseController : Controller
    {
        public new CustomClaimsPrincipal User => new CustomClaimsPrincipal(base.User);
    }
}
