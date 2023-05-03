namespace TestPlatform.Database.EntityTypeConfigurations.Questions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Questions;

    internal class QuestionTestMapETC : IEntityTypeConfiguration<QuestionTestMap>
    {
        public void Configure(EntityTypeBuilder<QuestionTestMap> builder)
        {
            builder
                .HasAlternateKey(qtm => new { qtm.QuestionId, qtm.TestId });

            builder
                .HasOne(qtm => qtm.Question)
                .WithMany(qc => qc.Tests)
                .HasForeignKey(qtm => qtm.QuestionId);

            builder
                .HasOne(qtm => qtm.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(qtm => qtm.TestId);
        }
    }
}
