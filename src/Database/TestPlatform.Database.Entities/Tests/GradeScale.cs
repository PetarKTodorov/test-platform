﻿namespace TestPlatform.Database.Entities.Tests
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;

    public class GradeScale : BaseEntity
    {
        public GradeScale()
        {
            this.Еvaluations = new HashSet<GradeScaleTestЕvaluationMap>();
        }

        [Required]
        [Range(Validations.ZERO, Validations.TWO_POWER_THIRTEEN)]
        public int LowerBound { get; set; }

        [Required]
        [Range(Validations.ZERO, Validations.TWO_POWER_THIRTEEN)]
        public int UpperBound { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Grade { get; set; }

        public virtual ICollection<GradeScaleTestЕvaluationMap> Еvaluations { get; set; }
    }
}
