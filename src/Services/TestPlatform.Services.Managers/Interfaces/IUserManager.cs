namespace TestPlatform.Services.Managers.Interfaces
{
    using Microsoft.AspNetCore.Http;

    using TestPlatform.DTOs.BindingModels.User;

    public interface IUserManager
    {
        Task RegisterAsync(RegisterUserBM model);

        Task<bool> LoginAsync(LoginUserBM model, HttpContext httpContext);
    }
}
