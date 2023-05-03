﻿namespace TestPlatform.Application.Areas.Account.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.DTOs.BindingModels.User;
    using TestPlatform.Services.Managers.Interfaces;

    public class UserController : BaseAccountController
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
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

            return this.RedirectToAction(actionName: "Index", controllerName: "Home", new { area = "" });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this.userManager.Logout(this.HttpContext);

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
