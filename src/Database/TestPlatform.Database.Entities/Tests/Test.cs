namespace TestPlatform.Database.Entities.Tests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Entities.Subjects;

    public class Test : BaseEntity
    {
        public Test()
        {
            this.IsApproved = false;
            this.Approvers = new HashSet<TestApprovalMap>();
            this.SubjectTags = new HashSet<TestSubjectTagMap>();
            this.Questions = new HashSet<QuestionTestMap>();
            this.Users = new HashSet<TestUserMap>();
            this.Rooms = new HashSet<Room>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Title { get; set; }

        [StringLength(maximumLength: Validations.TWO_POWER_SIXTEEN, MinimumLength = Validations.ONE)]
        public string Instructions { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        [Required]
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }

        public Guid? ЕvaluationId { get; set; }
        public virtual TestЕvaluation Еvaluation { get; set; }

        [Required]
        public bool HasRandomizeQuestions { get; set; }

        // TODO: calculate property
        [NotMapped]
        public double TotalPoints { get; set; }

        public virtual ICollection<TestApprovalMap> Approvers { get; set; }

        public virtual ICollection<TestSubjectTagMap> SubjectTags { get; set; }

        public virtual ICollection<QuestionTestMap> Questions { get; set; }

        public virtual ICollection<TestUserMap> Users { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
