namespace TestPlatform.Database.EntityTypeConfigurations.Authorization
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Authorization;

    internal class UserRoleMapETC : IEntityTypeConfiguration<UserRoleMap>
    {
        public void Configure(EntityTypeBuilder<UserRoleMap> builder)
        {
            builder
                .HasAlternateKey(urm => new { urm.UserId, urm.RoleId });

            builder
                .HasOne(urm => urm.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(urm => urm.UserId);

            builder
                .HasOne(urm => urm.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(urm => urm.RoleId);
        }
    }
}
