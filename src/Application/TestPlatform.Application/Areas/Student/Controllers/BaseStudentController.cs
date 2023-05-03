namespace TestPlatform.Application.Areas.Student.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Common.Constants;

    [Area(ApplicationAreas.STUDENT)]
    [Authorize(Roles = ApplicationRoles.STUDENT)]
    public class BaseStudentController : Controller
    {

    }
}
