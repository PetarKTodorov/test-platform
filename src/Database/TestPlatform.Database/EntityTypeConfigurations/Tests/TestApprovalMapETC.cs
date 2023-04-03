namespace TestPlatform.Database.EntityTypeConfigurations.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Tests;

    internal class TestApprovalMapETC : IEntityTypeConfiguration<TestApprovalMap>
    {
        public void Configure(EntityTypeBuilder<TestApprovalMap> builder)
        {
            builder
                .HasAlternateKey(tam => new { tam.TestId, tam.UserId });

            builder
                .HasOne(tam => tam.Test)
                .WithMany(t => t.Approvers)
                .HasForeignKey(tam => tam.TestId);

            builder
                .HasOne(tam => tam.User)
                .WithMany(u => u.ApprovedTests)
                .HasForeignKey(tam => tam.UserId);
        }
    }
}
