namespace TestPlatform.Database.EntityTypeConfigurations.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Tests;

    internal class GradeScaleTestEvaluationMapETC : IEntityTypeConfiguration<GradeScaleTestEvaluationMap>
    {
        public void Configure(EntityTypeBuilder<GradeScaleTestEvaluationMap> builder)
        {
            builder
                .HasAlternateKey(gstem => new { gstem.GradeScaleId, gstem.TestEvaluationId });

            builder
                .HasOne(gstem => gstem.GradeScale)
                .WithMany(gs => gs.Evaluations)
                .HasForeignKey(gstem => gstem.GradeScaleId);

            builder
                .HasOne(gstem => gstem.TestEvaluation)
                .WithMany(te => te.GradeScales)
                .HasForeignKey(gstem => gstem.TestEvaluationId);
        }
    }
}
