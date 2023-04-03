namespace TestPlatform.Database.EntityTypeConfigurations.Authorization
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Authorization;

    internal class RoleETC : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasIndex(r => r.Name)
                .IsUnique(true);
        }
    }
}
