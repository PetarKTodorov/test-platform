namespace TestPlatform.Database.EntityTypeConfigurations.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Tests;

    internal class TestETC : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder
                .HasOne(t => t.Status)
                .WithMany(s => s.Tests)
                .HasForeignKey(t => t.StatusId);

            builder
                .HasOne(t => t.Еvaluation)
                .WithOne(e => e.Test)
                .HasForeignKey<Test>(t => t.ЕvaluationId);
        }
    }
}
