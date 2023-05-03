namespace TestPlatform.Database.Entities.Subjects
{
    using System;

    using TestPlatform.Database.Entities.Authorization;

    public class UserSubjectTagMap : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid SubjectTagId { get; set; }
        public virtual SubjectTag SubjectTag { get; set; }
    }
}
