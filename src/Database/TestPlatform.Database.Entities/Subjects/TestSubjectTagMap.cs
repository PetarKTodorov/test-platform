namespace TestPlatform.Database.Entities.Subjects
{
    using System;

    using TestPlatform.Database.Entities.Tests;

    public class TestSubjectTagMap : BaseEntity
    {
        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }

        public Guid SubjectTagId { get; set; }
        public virtual SubjectTag SubjectTag { get; set; }
    }
}
