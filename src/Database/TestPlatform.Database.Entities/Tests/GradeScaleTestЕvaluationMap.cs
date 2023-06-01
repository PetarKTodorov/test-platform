namespace TestPlatform.Database.Entities.Tests
{
    using System;

    public class GradeScaleTestEvaluationMap : BaseEntity
    {
        public Guid GradeScaleId { get; set; }
        public virtual GradeScale GradeScale { get; set; }

        public Guid TestEvaluationId { get; set; }
        public virtual TestEvaluation TestEvaluation { get; set; }
    }
}
