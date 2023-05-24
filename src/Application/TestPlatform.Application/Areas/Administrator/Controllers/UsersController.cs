namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Database.Entities.Authorization;
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
            var usersCount = await this.userService.GetCountOfAllAsyns();
            var paging = new Paging(page.Value, 1, usersCount);

            var users = await this.userService.FindAllAsync<UserInformationVM>(paging.CurrentPage, paging.PageSize);
            var result = new PageableResult<UserInformationVM>(users, paging);

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

        [HttpGet]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var user = await this.userService.FindByIdAsync<UserInformationVM>(userId);

            return this.Json(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid userId)
        {
            await this.userService.DeleteAsync<User>(userId);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Restore(Guid userId)
        {
            var user = await this.userService.FindByIdAsync<UserInformationVM>(userId, isDeletedFlag: true);

            return this.Json(user);
        }

        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(Guid userId)
        {
            await this.userService.RestoryAsync<User>(userId);

            return this.RedirectToAction("Index");
        }
    }
}
