namespace TestPlatform.Database.Entities.Tests
{
    public class GradeScale : BaseEntity
    {
        public GradeScale()
        {
            this.Еvaluations = new HashSet<GradeScaleTestЕvaluationMap>();
        }

        public int LowerBound { get; set; }

        public int UpperBound { get; set; }

        public string Grade { get; set; }

        public virtual ICollection<GradeScaleTestЕvaluationMap> Еvaluations { get; set; }
    }
}
