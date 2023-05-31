namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.DTOs.ViewModels.Roles;
    using TestPlatform.DTOs.ViewModels.Subjects;
    using TestPlatform.DTOs.ViewModels.Users;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class UsersController : BaseAdministratorController
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IUserRoleMapService userRoleMapService;
        private readonly ISubjectTagService subjectTagService;
        private readonly IUserSubjectTagMapService userSubjectTagMapService;
        private readonly IUserManager userManager;
        private readonly ISearchPageableMananager searchPageableMananager;

        public UsersController(IUserService userService,
            IRoleService roleService,
            IUserRoleMapService userRoleMapService,
            ISubjectTagService subjectTagService,
            IUserSubjectTagMapService userSubjectTagMapService,
            IUserManager userManager,
            ISearchPageableMananager searchPageableMananager)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.userRoleMapService = userRoleMapService;
            this.subjectTagService = subjectTagService;
            this.userSubjectTagMapService = userSubjectTagMapService;
            this.userManager = userManager;
            this.searchPageableMananager = searchPageableMananager;
        }

        [HttpGet]
        public async Task<IActionResult> List(ICollection<AbstractSearch> searchCriteria, int? page = 1)
        {
            var dataQuery = this.userService.FindAllUsersAsQueryable<UserInformationVM>();
            var model = this.searchPageableMananager.CreateSearchFilterModelWithPaging(dataQuery, searchCriteria, page.Value);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid userId)
        {
            var result = new UpdateUserVM();
            result.AllRoles = await this.roleService.FindAllAsync<RoleVM>(false);
            result.UserRoles = await this.userRoleMapService.FindUserRolesAsync<UserRoleMapVM>(userId);
            result.AllSubjectTags = await this.subjectTagService.FindAllAsync<SubjectTagVm>(false);
            result.UserSubjectTags = await this.userSubjectTagMapService.FindUserSubjectTagsAsync<UpdateUserSubjectTagMapVM>(userId);

            return this.Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid userId, IEnumerable<Guid> userRoles, IEnumerable<Guid> userSubjectTags)
        {
            var currentUserId = new Guid(this.User.FindFirstValue(UserClaimTypes.ID));
            await this.userRoleMapService.UpdateUserRolesAsync(userId, userRoles, currentUserId);
            await this.userSubjectTagMapService.UpdateUserSubjectTagsAsync(userId, userSubjectTags, currentUserId);

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
