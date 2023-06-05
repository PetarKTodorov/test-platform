namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsTestEvaluationVM : IMapFrom<TestEvaluation>
    {
        [DisplayName("Will round the grade")]
        public bool IsRouned { get; set; }

        [DisplayName("Grade scale")]
        public ICollection<DetailsGradeScaleVM> GradeScales { get; set; }
    }
}
