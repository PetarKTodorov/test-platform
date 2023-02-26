namespace TestPlatform.Database.Entities.Subjects
{
    using TestPlatform.Database.Entities.Questions;

    public class SubjectTag : BaseEntity
    {
        public SubjectTag()
        {
            this.Questions = new HashSet<QuestionCopy>();
            this.Users = new HashSet<UserSubjectTagMap>();
            this.Tests = new HashSet<TestSubjectTagMap>();
        }

        public string Name { get; set; }

        public virtual ICollection<QuestionCopy> Questions { get; set; }

        public virtual ICollection<UserSubjectTagMap> Users { get; set; }

        public virtual ICollection<TestSubjectTagMap> Tests { get; set; }
    }
}
