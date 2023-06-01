namespace TestPlatform.Services.Database.Test.Interfaces
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Database.Interfaces;

    public interface ITestEvaluationService : IBaseService<TestEvaluation>
    {
        Task<T> FindTestEvaluationByTestIdAsync<T>(Guid testId);
    }
}
