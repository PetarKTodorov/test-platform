namespace TestPlatform.Application.Areas.Account.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Common.Constants;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.DTOs.BindingModels.User;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class UserController : BaseAccountController
    {
        private readonly IUserManager userManager;
        private readonly IUserService userService;

        public UserController(IUserManager userManager,
            IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        [CustomAllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return this.View();
        }

        [CustomAllowAnonymous]
        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserBM model)
        {
            bool isSucceeded = await this.userManager.RegisterAsync(model);

            if (isSucceeded == false)
            {
                this.ViewBag.RegisterError = ErrorMessages.EMAIL_IS_ALREADY_USED_ERROR_MESSAGE;

                return this.View(model);
            }

            return this.RedirectToAction(nameof(Login));
        }

        [CustomAllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return this.View();
        }

        [CustomAllowAnonymous]
        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserBM model)
        {
            bool isSucceeded = await this.userManager.LoginAsync(model, this.HttpContext);

            if (isSucceeded == false)
            {
                this.ViewBag.LoginError = ErrorMessages.INVALID_CREDENTIALS_ERROR_MESSAGE;

                return this.View(model);
            }

            return this.RedirectToAction(actionName: "Index", controllerName: "Home", new { area = "" });
        }

        [CustomAuthorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this.userManager.Logout(this.HttpContext);

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [CustomAuthorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await this.userService.FindByIdAsync<ProfileUserBM>(this.CurrentUserId);

            return this.View(user);
        }

        [CustomAuthorize]
        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileUserBM model)
        {
            if (this.CurrentUserId != model.Id)
            {
                return this.NotFound();
            }

            var user = await this.userService.FindByIdAsync<ProfileUserBM>(model.Id);
            if (user == null)
            {
                return this.NotFound();
            }

            model.Email = user.Email;
            await this.userService.UpdateAsync<BaseBM, ProfileUserBM>(model.Id, model, this.CurrentUserId);

            return this.RedirectToAction(nameof(Profile));
        }
    }
}
