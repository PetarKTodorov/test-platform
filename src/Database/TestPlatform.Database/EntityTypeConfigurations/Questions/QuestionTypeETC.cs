namespace TestPlatform.Database.EntityTypeConfigurations.Questions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Questions;

    internal class QuestionTypeETC : IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            builder
                .HasIndex(qt => qt.Name)
                .IsUnique(true);
        }
    }
}
