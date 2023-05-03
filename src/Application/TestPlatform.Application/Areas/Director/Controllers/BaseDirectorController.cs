namespace TestPlatform.Application.Areas.Director.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Common.Constants;

    [Area(ApplicationAreas.DIRECTOR)]
    [Authorize(Roles = ApplicationRoles.DIRECTOR)]
    public class BaseDirectorController : Controller
    {

    }
}
