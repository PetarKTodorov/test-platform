namespace TestPlatform.Services.Managers.Interfaces
{
    using TestPlatform.DTOs.BindingModels.User;

    public interface IUserManager
    {
        Task RegisterAsync(RegisterUserBM model);

        Task LoginAsync(LoginUserBM model);
    }
}
