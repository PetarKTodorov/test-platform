namespace TestPlatform.Database.EntityTypeConfigurations.Subjects
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Subjects;

    internal class SubjectTagETC : IEntityTypeConfiguration<SubjectTag>
    {
        public void Configure(EntityTypeBuilder<SubjectTag> builder)
        {
            builder
               .HasIndex(st => st.Name)
               .IsUnique(true);
        }
    }
}
