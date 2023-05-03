namespace TestPlatform.Database.EntityTypeConfigurations.Subjects
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Subjects;

    internal class UserSubjectTagMapETC : IEntityTypeConfiguration<UserSubjectTagMap>
    {
        public void Configure(EntityTypeBuilder<UserSubjectTagMap> builder)
        {
            builder
                .HasAlternateKey(ustm => new { ustm.UserId, ustm.SubjectTagId });

            builder
                .HasOne(ustm => ustm.User)
                .WithMany(u => u.SubjectTags)
                .HasForeignKey(ustm => ustm.UserId);

            builder
                .HasOne(ustm => ustm.SubjectTag)
                .WithMany(st => st.Users)
                .HasForeignKey(ustm => ustm.SubjectTagId);
        }
    }
}
