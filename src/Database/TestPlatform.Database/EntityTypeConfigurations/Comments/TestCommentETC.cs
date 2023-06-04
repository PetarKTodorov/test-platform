namespace TestPlatform.Database.EntityTypeConfigurations.Comments
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TestPlatform.Database.Entities.Comments;

    internal class TestCommentETC : IEntityTypeConfiguration<TestComment>
    {
        public void Configure(EntityTypeBuilder<TestComment> builder)
        {
            builder
                .HasOne(tc => tc.Test)
                .WithMany(t => t.Comments)
                .HasForeignKey(tc => tc.TestId);

            builder
                .HasOne(tc => tc.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(tc => tc.CreatedBy);

            builder
                .HasOne(tc => tc.ParentComment)
                .WithMany(t => t.Comments)
                .HasForeignKey(tc => tc.ParentCommentId);
        }
    }
}
