namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.DTOs.ViewModels.Roles;
    using TestPlatform.DTOs.ViewModels.Users;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class UsersController : BaseAdministratorController
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IUserManager userManager;
        private readonly ISearchPageableMananager searchPageableMananager;

        public UsersController(IUserService userService,
            IRoleService roleService,
            IUserManager userManager,
            ISearchPageableMananager searchPageableMananager)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.userManager = userManager;
            this.searchPageableMananager = searchPageableMananager;
        }

        [HttpGet]
        public async Task<IActionResult> List(int? page = 1)
        {
            var dataQuery = this.userService.FindAllAsQueryable<UserInformationVM>();
            var model = this.searchPageableMananager.CreatePageableResult(dataQuery, page.Value);

            return this.View(model);
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
            var currentUserId = new Guid(this.User.FindFirstValue(UserClaimTypes.ID));
            await this.userManager.UpdateUserRolesAsync(userId, userRoles, currentUserId);

            return this.RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var user = await this.userService.FindByIdAsync<UserInformationVM>(userId);

            return this.Json(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid userId)
        {
            var currentUserId = new Guid(this.User.FindFirstValue(UserClaimTypes.ID));
            await this.userService.DeleteAsync<User>(userId, currentUserId);

            return this.RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Restore(Guid userId)
        {
            var user = await this.userService.FindByIdAsync<UserInformationVM>(userId, isDeletedFlag: true);

            return this.Json(user);
        }

        [HttpPost, ActionName("Restore")]
        public async Task<IActionResult> RestoreConfirmed(Guid userId)
        {
            var currentUserId = new Guid(this.User.FindFirstValue(UserClaimTypes.ID));
            await this.userService.RestoryAsync<User>(userId, currentUserId);

            return this.RedirectToAction(nameof(List));
        }
    }
}
