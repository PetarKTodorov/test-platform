namespace TestPlatform.Database.EntityTypeConfigurations.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Tests;

    internal class GradeScaleTestЕvaluationMapETC : IEntityTypeConfiguration<GradeScaleTestЕvaluationMap>
    {
        public void Configure(EntityTypeBuilder<GradeScaleTestЕvaluationMap> builder)
        {
            builder
                .HasAlternateKey(gstem => new { gstem.GradeScaleId, gstem.TestЕvaluationId });

            builder
                .HasOne(gstem => gstem.GradeScale)
                .WithMany(gs => gs.Еvaluations)
                .HasForeignKey(gstem => gstem.GradeScaleId);

            builder
                .HasOne(gstem => gstem.TestЕvaluation)
                .WithMany(te => te.GradeScales)
                .HasForeignKey(gstem => gstem.TestЕvaluationId);
        }
    }
}
