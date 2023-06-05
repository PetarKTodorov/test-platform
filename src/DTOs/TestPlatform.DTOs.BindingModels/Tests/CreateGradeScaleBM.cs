namespace TestPlatform.DTOs.BindingModels.Tests
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateGradeScaleBM : IMapTo<GradeScale>
    {
        [Required]
        [Range(Validations.ZERO, Validations.TWO_POWER_THIRTEEN)]
        public int LowerBound { get; set; }

        [Required]
        [Range(Validations.ZERO, Validations.TWO_POWER_THIRTEEN)]
        public int UpperBound { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Grade { get; set; }
    }
}
