namespace TestPlatform.Database.EntityTypeConfigurations.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Tests;

    internal class TestЕvaluationETC : IEntityTypeConfiguration<TestЕvaluation>
    {
        public void Configure(EntityTypeBuilder<TestЕvaluation> builder)
        {
            builder
                .HasOne(te => te.Test)
                .WithOne(t => t.Еvaluation)
                .HasForeignKey<TestЕvaluation>(te => te.TestId);
        }
    }
}
