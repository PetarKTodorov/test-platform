namespace TestPlatform.Database.Entities.Tests
{
    using System;

    public class GradeScaleTestЕvaluationMap : BaseEntity
    {
        public Guid GradeScaleId { get; set; }
        public virtual GradeScale GradeScale { get; set; }

        public Guid TestЕvaluationId { get; set; }
        public virtual TestЕvaluation TestЕvaluation { get; set; }
    }
}
