namespace TestPlatform.Database.EntityTypeConfigurations.Questions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Questions;

    internal class QuestionETC : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            //builder
            //    .HasIndex(q => q.Title)
            //    .IsUnique(true);
        }
    }
}
