namespace TestPlatform.Database.Entities.Comments
{
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Entities.Tests;

    public class TestComment : BaseEntity
    {
        public TestComment()
        {
            this.Comments = new HashSet<TestComment>();
        }

        public string Content { get; set; }

        public virtual User User { get; set; }

        public Guid TestId { get; set; }

        public virtual Test Test { get; set; }

        public Guid? ParentCommentId { get; set; }

        public virtual TestComment ParentComment { get; set; }

        public virtual ICollection<TestComment> Comments { get; set; }
    }
}
