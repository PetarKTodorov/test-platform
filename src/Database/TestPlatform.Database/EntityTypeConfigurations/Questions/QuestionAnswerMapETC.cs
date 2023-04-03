namespace TestPlatform.Database.EntityTypeConfigurations.Questions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Questions;

    internal class QuestionAnswerMapETC : IEntityTypeConfiguration<QuestionAnswerMap>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswerMap> builder)
        {
            builder
                .HasAlternateKey(qam => new { qam.QuestionId, qam.AnswerId });

            builder
                .HasOne(qam => qam.Question)
                .WithMany(qc => qc.Answers)
                .HasForeignKey(qam => qam.QuestionId);

            builder
                .HasOne(qam => qam.Answer)
                .WithMany(a => a.Questions)
                .HasForeignKey(qam => qam.AnswerId);
        }
    }
}
