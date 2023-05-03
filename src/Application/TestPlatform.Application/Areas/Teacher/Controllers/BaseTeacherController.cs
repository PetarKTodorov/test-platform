namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Common.Constants;

    [Area(ApplicationAreas.TEACHER)]
    [CustomAuthorize(ApplicationRoles.TEACHER)]
    public class BaseTeacherController : Controller
    {

    }
}
