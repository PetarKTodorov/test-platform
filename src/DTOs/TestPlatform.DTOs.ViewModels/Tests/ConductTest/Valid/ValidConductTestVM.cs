namespace TestPlatform.DTOs.ViewModels.Tests.ConductTest.Valid
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ValidConductTestVM : IMapFrom<Test>
    {
        public Guid Id { get; set; }

        public DetailsTestEvaluationVM Evaluation { get; set; }

        public virtual ICollection<ValidQuestionTestMapVM> Questions { get; set; }
    }
}
