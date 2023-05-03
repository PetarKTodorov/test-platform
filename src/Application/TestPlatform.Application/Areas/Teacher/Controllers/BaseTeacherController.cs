namespace TestPlatform.Application.Areas.Teacher.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Common.Constants;

    [Area(ApplicationAreas.TEACHER)]
    [Authorize(Roles = ApplicationRoles.TEACHER)]
    public class BaseTeacherController : Controller
    {

    }
}
