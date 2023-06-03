namespace TestPlatform.Services.Managers
{
    using TestPlatform.Common.Constants;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.DTOs.BindingModels.Tests;
    using TestPlatform.DTOs.ViewModels.Tests;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class TestGradeScaleManager : ITestGradeScaleManager
    {
        private readonly IGradeScaleService gradeScaleService;
        private readonly ITestEvaluationService testEvaluationService;
        private readonly IGradeScaleTestEvaluationMapService gradeScaleTestEvaluationMapService;

        public TestGradeScaleManager(
            IGradeScaleService gradeScaleService,
            ITestEvaluationService testEvaluationService,
            IGradeScaleTestEvaluationMapService gradeScaleTestEvaluationMapService)
        {
            this.gradeScaleService = gradeScaleService;
            this.testEvaluationService = testEvaluationService;
            this.gradeScaleTestEvaluationMapService = gradeScaleTestEvaluationMapService;
        }

        public async Task CreateGradeScaleForTestAsync(Guid testId, int totalPoints, Guid userId)
        {
            var createdTestEvaluation = await this.CreateTestEvaluationAsync(testId, userId);

            await this.CreateGradeScalesAsync(totalPoints, userId, createdTestEvaluation.Id);
        }

        public async Task DeleteGradeScalesAsync(Guid testId, Guid userId)
        {
            var testWithGradeScales = await this.testEvaluationService.FindTestEvaluationByTestIdAsync<TestEvaluationGradeScaleVM>(testId);

            foreach (var gradeScaleTestEvaluation in testWithGradeScales.GradeScales)
            {
                await this.gradeScaleTestEvaluationMapService.HardDeleteAsync<BaseBM>(gradeScaleTestEvaluation.Id);
            }

            foreach (var gradeScaleTestEvaluation in testWithGradeScales.GradeScales)
            {
                await this.gradeScaleService.HardDeleteAsync<BaseBM>(gradeScaleTestEvaluation.GradeScaleId);
            }

            await this.testEvaluationService.HardDeleteAsync<BaseBM>(testWithGradeScales.Id);
        }

        private async Task CreateGradeScalesAsync(int totalPoints, Guid userId, Guid testEvaluationId)
        {
            var gradePointsStep = totalPoints / GlobalConstants.COUNT_OF_GRADES;

            var lowestGrade = 2;
            var points = 0;
            for (int grade = lowestGrade; grade < lowestGrade + GlobalConstants.COUNT_OF_GRADES; grade++)
            {
                var lowerGradeBound = points;
                var upperGradeBound = points + gradePointsStep - 1;

                if (grade == lowestGrade + GlobalConstants.COUNT_OF_GRADES - 1)
                {
                    upperGradeBound = totalPoints;
                }

                var createdGradeScale = await this.CreateGradeScaleAsync(userId, grade, lowerGradeBound, upperGradeBound);

                await this.CreateGradeScaleTestEvaluationAsync(userId, testEvaluationId, createdGradeScale.Id);

                // Incremenet the points for the next grade
                points = upperGradeBound + 1;
            }
        }

        private async Task<BaseBM> CreateGradeScaleAsync(Guid userId, int grade, int lowerGradeBound, int upperGradeBound)
        {
            var gradeScale = new CreateGradeScaleBM()
            {
                LowerBound = lowerGradeBound,
                UpperBound = upperGradeBound,
                Grade = grade.ToString(),
            };

            return await this.gradeScaleService.CreateAsync<BaseBM, CreateGradeScaleBM>(gradeScale, userId);
        }

        private async Task CreateGradeScaleTestEvaluationAsync(Guid userId, Guid testEvaluationId, Guid gradeScaleId)
        {
            var gradeScaleTestEvaluation = new CreateGradeScaleTestEvaluationBM()
            {
                GradeScaleId = gradeScaleId,
                TestEvaluationId = testEvaluationId,
            };
            await this.gradeScaleTestEvaluationMapService
                .CreateAsync<BaseBM, CreateGradeScaleTestEvaluationBM>(gradeScaleTestEvaluation, userId);
        }

        private async Task<BaseBM> CreateTestEvaluationAsync(Guid testId, Guid userId)
        {
            var testEvaluation = new CreateTestEvaluationBM()
            {
                IsRouned = false,
                TestId = testId,
            };

            return await this.testEvaluationService.CreateAsync<BaseBM, CreateTestEvaluationBM>(testEvaluation, userId);
        }
    }
}
