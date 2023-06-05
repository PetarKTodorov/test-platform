namespace TestPlatform.Database.Entities.Authorization
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Comments;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Entities.Tests;

    public class User : BaseEntity
    {
        public User()
        {
            this.IsEmailConfirmed = false;
            this.Roles = new HashSet<UserRoleMap>();
            this.Rooms = new HashSet<RoomParticipantMap>();
            this.ApprovedTests = new HashSet<TestApprovalMap>();
            this.SubjectTags = new HashSet<UserSubjectTagMap>();
            this.Tests = new HashSet<TestUserMap>();
            this.ChatMessages = new HashSet<ChatMessage>();
            this.Comments = new HashSet<TestComment>();
        }

        [Required]
        [EmailAddress]
        [RegularExpression(Validations.EMAIL_REGEX)]
        public string Email { get; set; }

        [Required]
        public bool IsEmailConfirmed { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{this.FirstName} {this.MiddleName} {this.LastName}";

        [Required]
        [RegularExpression(Validations.PASSWORD_REGEX)]
        public string Password { get; set; }

        public virtual ICollection<UserRoleMap> Roles { get; set; }

        public virtual ICollection<RoomParticipantMap> Rooms { get; set; }

        public virtual ICollection<TestApprovalMap> ApprovedTests { get; set; }

        public virtual ICollection<UserSubjectTagMap> SubjectTags { get; set; }

        public virtual ICollection<TestUserMap> Tests { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }

        public virtual ICollection<TestComment> Comments { get; set; }
    }
}
