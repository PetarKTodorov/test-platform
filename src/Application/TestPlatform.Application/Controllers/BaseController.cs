namespace TestPlatform.Application.Controllers
{
    using System.Security.Claims;
    
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.ApplicationUser;

    public class BaseController : Controller
    {
        public new CustomClaimsPrincipal User => new CustomClaimsPrincipal(base.User);

        protected Guid CurrentUserId => Guid.Parse(this.User.FindFirstValue(UserClaimTypes.ID));
    }
}
