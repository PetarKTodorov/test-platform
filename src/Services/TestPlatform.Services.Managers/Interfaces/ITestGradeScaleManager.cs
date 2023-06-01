namespace TestPlatform.Services.Managers.Interfaces
{
    public interface ITestGradeScaleManager
    {
        Task CreateGradeScaleForTestAsync(Guid testId, int totalPoints, Guid userId);
    }
}
