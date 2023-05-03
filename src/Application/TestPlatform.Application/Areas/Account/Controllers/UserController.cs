namespace TestPlatform.Application.Areas.Account.Controllers
{
    using System.Security.Claims;
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.DTOs.BindingModels.User;

    using TestPlatform.Services.Managers.Interfaces;

    public class UserController : BaseAccountController
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            string test = Encoding.UTF8.GetString(this.HttpContext.Session.Get("UserEmail"));

            return this.View();
        }

        [AllowAnonymous]
        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserBM model)
        {
            await this.userManager.RegisterAsync(model);

            return this.RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return this.View();
        }

        [AllowAnonymous]
        [ValidateModelState]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserBM model)
        {
            bool isSucceeded = await this.userManager.LoginAsync(model, this.HttpContext);

            if (isSucceeded == false)
            {
                this.ViewBag.LoginError = "Invalid password or email.";

                return this.View(model);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, "aaa"),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            this.SignIn(principal);

            return this.RedirectToAction(actionName: "Index", controllerName: "Home", new { area = "" });
        }
    }
}
