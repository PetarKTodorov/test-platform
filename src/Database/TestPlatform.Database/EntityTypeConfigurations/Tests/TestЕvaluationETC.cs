namespace TestPlatform.Database.EntityTypeConfigurations.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Tests;

    internal class TestЕvaluationETC : IEntityTypeConfiguration<TestEvaluation>
    {
        public void Configure(EntityTypeBuilder<TestEvaluation> builder)
        {
            builder
                .HasOne(te => te.Test)
                .WithOne(t => t.Еvaluation)
                .HasForeignKey<TestEvaluation>(te => te.TestId);
        }
    }
}
