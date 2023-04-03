namespace TestPlatform.Database.EntityTypeConfigurations.Subjects
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Subjects;

    internal class TestSubjectTagMapETC : IEntityTypeConfiguration<TestSubjectTagMap>
    {
        public void Configure(EntityTypeBuilder<TestSubjectTagMap> builder)
        {
            builder
                .HasAlternateKey(tstm => new { tstm.TestId, tstm.SubjectTagId });

            builder
                .HasOne(tstm => tstm.Test)
                .WithMany(t => t.SubjectTags)
                .HasForeignKey(tstm => tstm.TestId);

            builder
                .HasOne(tstm => tstm.SubjectTag)
                .WithMany(st => st.Tests)
                .HasForeignKey(tstm => tstm.SubjectTagId);
        }
    }
}
