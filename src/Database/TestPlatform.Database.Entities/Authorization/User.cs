namespace TestPlatform.Database.Entities.Authorization
{
    using System.Collections.Generic;

    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Entities.Tests;

    public class User : BaseEntity
    {
        public User()
        {
            this.Roles = new HashSet<UserRoleMap>();
            this.Rooms = new HashSet<RoomParticipantMap>();
            this.ApprovedTests = new HashSet<TestApprovalMap>();
            this.SubjectTags = new HashSet<UserSubjectTagMap>();
            this.Tests = new HashSet<TestUserMap>();
        }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public virtual ICollection<UserRoleMap> Roles { get; set; }

        public virtual ICollection<RoomParticipantMap> Rooms { get; set; }

        public virtual ICollection<TestApprovalMap> ApprovedTests { get; set; }

        public virtual ICollection<UserSubjectTagMap> SubjectTags { get; set; }

        public virtual ICollection<TestUserMap> Tests { get; set; }
    }
}
