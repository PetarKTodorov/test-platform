namespace TestPlatform.Database.EntityTypeConfigurations.Questions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Questions;

    internal class QuestionCopyETC : IEntityTypeConfiguration<QuestionCopy>
    {
        public void Configure(EntityTypeBuilder<QuestionCopy> builder)
        {
            builder
                .HasOne(qc => qc.OriginalQuestion)
                .WithMany(q => q.Copies)
                .HasForeignKey(qc => qc.OriginalQuestionId);

            builder
                .HasOne(qc => qc.QuestionType)
                .WithMany(qt => qt.Questions)
                .HasForeignKey(qc => qc.QuestionTypeId);

            builder
                .HasOne(qc => qc.SubjectTag)
                .WithMany(st => st.Questions)
                .HasForeignKey(qc => qc.SubjectTagId);
        }
    }
}
