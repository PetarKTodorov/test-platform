namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System.ComponentModel;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsGradeScaleVM : IMapFrom<GradeScaleTestEvaluationMap>
    {
        [DisplayName("Lower bound")]
        public int GradeScaleLowerBound { get; set; }

        [DisplayName("Upper bound")]
        public int GradeScaleUpperBound { get; set; }

        [DisplayName("Grade")]
        public string GradeScaleGrade { get; set; }
    }
}
