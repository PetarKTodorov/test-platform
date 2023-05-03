namespace TestPlatform.Application.Areas.Director.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Common.Constants;

    [Area(ApplicationAreas.DIRECTOR)]
    [CustomAuthorize(ApplicationRoles.DIRECTOR)]
    public class BaseDirectorController : Controller
    {

    }
}
