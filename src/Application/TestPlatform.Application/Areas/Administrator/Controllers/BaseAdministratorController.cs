namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Common.Constants;

    [Area(ApplicationAreas.ADMINISTRATOR)]
    [Authorize(Roles = ApplicationRoles.ADMINISTRATOR)]
    public class BaseAdministratorController : Controller
    {

    }
}
