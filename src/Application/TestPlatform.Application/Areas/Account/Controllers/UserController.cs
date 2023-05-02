namespace TestPlatform.Application.Areas.Account.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.DTOs.BindingModels.User;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    public class UserController : BaseAccountController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserBM model)
        {
            return View();
        }
    }
}
