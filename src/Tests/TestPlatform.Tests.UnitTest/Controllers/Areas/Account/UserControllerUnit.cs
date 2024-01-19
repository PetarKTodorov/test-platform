namespace TestPlatform.Tests.UnitTest.Controllers.Areas.Account
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using TestPlatform.Application.Areas.Account.Controllers;
    using TestPlatform.DTOs.BindingModels.User;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    internal class UserControllerUnit
    {
        private UserController userController;
        private Mock<IUserManager> mockUserManager;
        private Mock<IUserService> mockUserService;

        [SetUp]
        public void Setup()
        {
            this.mockUserManager = new Mock<IUserManager>();
            this.mockUserService = new Mock<IUserService>();

            this.userController = new UserController(
                this.mockUserManager.Object, this.mockUserService.Object);
        }

        [Test]
        [TestCase("administrator@email.mail", "1234")]
        [TestCase("test@email.mail", "12345678")]
        [TestCase("", "")]
        public async Task Login_Post_With_Invalid_Data(string email, string password)
        {
            // Arrange
            var loginModel = new LoginUserBM { Email = email, Password = password };
            this.mockUserManager.Setup(x => x.LoginAsync(loginModel, It.IsAny<HttpContext>())).ReturnsAsync(false);

            // Act
            var result = await this.userController.Login(loginModel) as RedirectToActionResult;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        [TestCase("administrator@email.mail", "12msHZ&!srd")]
        [TestCase("teacher1@email.mail", "12msHZ&!srd")]
        public async Task Login_Post_With_Valid_Data(string email, string password)
        {
            // Arrange
            var loginModel = new LoginUserBM { Email = email, Password = password };
            this.mockUserManager.Setup(x => x.LoginAsync(loginModel, It.IsAny<HttpContext>())).ReturnsAsync(true);

            // Act
            var result = await this.userController.Login(loginModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
            Assert.AreEqual(result.RouteValues["area"], "");
        }
    }
}
