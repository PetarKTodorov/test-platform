namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Controllers;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Common.Constants;

    [Area(ApplicationAreas.ADMINISTRATOR)]
    [CustomAuthorize(ApplicationRoles.ADMINISTRATOR)]
    public class BaseAdministratorController : BaseController
    {

    }
}
