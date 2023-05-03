namespace TestPlatform.Database.Entities.Tests
{
    using System.ComponentModel.DataAnnotations;

    public class TestЕvaluation : BaseEntity
    {
        public TestЕvaluation()
        {
            this.GradeScales = new HashSet<GradeScaleTestЕvaluationMap>();
        }

        // If is true and letter add plus (+) or minus(-) B+ if is true and number 3.45 
        [Required]
        public bool IsRouned { get; set; }

        public Guid? TestId { get; set; }
        public virtual Test Test { get; set; }

        public virtual ICollection<GradeScaleTestЕvaluationMap> GradeScales { get; set; }
    }
}
