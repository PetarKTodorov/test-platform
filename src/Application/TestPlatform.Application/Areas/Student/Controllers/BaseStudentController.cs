namespace TestPlatform.Application.Areas.Student.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Common.Constants;

    [Area(ApplicationAreas.STUDENT)]
    [CustomAuthorize(ApplicationRoles.STUDENT)]
    public class BaseStudentController : Controller
    {

    }
}
