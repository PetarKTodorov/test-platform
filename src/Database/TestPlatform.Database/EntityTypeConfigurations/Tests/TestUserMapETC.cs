namespace TestPlatform.Database.EntityTypeConfigurations.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Tests;

    internal class TestUserMapETC : IEntityTypeConfiguration<TestUserMap>
    {
        public void Configure(EntityTypeBuilder<TestUserMap> builder)
        {
            builder
                .HasAlternateKey(tum => new { tum.TestId, tum.UserId });

            builder
                .HasOne(tum => tum.Test)
                .WithMany(t => t.Users)
                .HasForeignKey(tum => tum.TestId);

            builder
                .HasOne(tum => tum.User)
                .WithMany(u => u.Tests)
                .HasForeignKey(tum => tum.UserId);
        }
    }
}
