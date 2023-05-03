namespace TestPlatform.Database.Entities.Tests
{
    using System;

    using TestPlatform.Database.Entities.Authorization;

    public class TestApprovalMap : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}
