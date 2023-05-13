namespace TestPlatform.Application.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.DTOs.ViewModels.Common;
    using TestPlatform.DTOs.ViewModels.Users;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    public class UsersController : BaseAdministratorController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }

            var users = await this.userService.FindAllAsync<UserInformationViewModel>(page.Value);
            var usersCount = await this.userService.GetCountOfAllAsyns();

            var result = new PageableResult<UserInformationViewModel>();
            result.Results = users;
            result.AllResultsCount = usersCount;
            result.CurrentPage = page.Value;

            return this.View(result);
        }
    }
}
