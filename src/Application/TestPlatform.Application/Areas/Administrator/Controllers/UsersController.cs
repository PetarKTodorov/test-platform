namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.DTOs.ViewModels.Common;
    using TestPlatform.DTOs.ViewModels.Roles;
    using TestPlatform.DTOs.ViewModels.Users;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class UsersController : BaseAdministratorController
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IUserManager userManager;

        public UsersController(IUserService userService,
            IRoleService roleService,
            IUserManager userManager)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }

            var result = new PageableResult<UserInformationVM>();
            var users = await this.userService.FindAllAsync<UserInformationVM>(page.Value, result.PageSize);
            var usersCount = await this.userService.GetCountOfAllAsyns();

            result.Results = users;
            result.AllResultsCount = usersCount;
            result.CurrentPage = page.Value;

            return this.View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ModifyUserRoles(Guid userId)
        {
            var userWithRoles = await this.userService.FindUserRolesAsync<UserRolesVM>(userId);

            var result = new ModifyUserRolesVM();
            result.AllRoles = await this.roleService.FindAllAsync<RoleVM>(false);
            result.UserRoles = userWithRoles.Roles;

            return this.Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyUserRoles(Guid userId, IEnumerable<Guid> userRoles)
        {
            await this.userManager.UpdateUserRolesAsync(userId, userRoles);

            return this.RedirectToAction("Index");
        }
    }
}
