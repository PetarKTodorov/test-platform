namespace TestPlatform.Database.Entities.Tests
{
    using System;

    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Entities.Subjects;

    public class Test : BaseEntity
    {
        public Test()
        {
            this.Approvers = new HashSet<TestApprovalMap>();
            this.SubjectTags = new HashSet<TestSubjectTagMap>();
            this.Questions = new HashSet<QuestionTestMap>();
            this.Users = new HashSet<TestUserMap>();
        }

        public string Instructions { get; set; }

        public bool IsApproved { get; set; }

        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }

        public Guid? ЕvaluationId { get; set; }
        public virtual TestЕvaluation Еvaluation { get; set; }

        public bool HasRandomizeQuestions { get; set; }

        // TODO calculate property
        public double TotalPoints { get; set; }

        public virtual ICollection<TestApprovalMap> Approvers { get; set; }

        public virtual ICollection<TestSubjectTagMap> SubjectTags { get; set; }

        public virtual ICollection<QuestionTestMap> Questions { get; set; }

        public virtual ICollection<TestUserMap> Users { get; set; }
    }
}
