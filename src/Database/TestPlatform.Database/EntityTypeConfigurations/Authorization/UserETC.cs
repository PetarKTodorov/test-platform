namespace TestPlatform.Database.EntityTypeConfigurations.Authorization
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Authorization;

    internal class UserETC : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(u => u.Email)
                .IsUnique(true);
        }
    }
}
