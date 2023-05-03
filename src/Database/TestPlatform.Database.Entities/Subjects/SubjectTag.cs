namespace TestPlatform.Database.Entities.Subjects
{
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;

    public class SubjectTag : BaseEntity
    {
        public SubjectTag()
        {
            this.Questions = new HashSet<QuestionCopy>();
            this.Users = new HashSet<UserSubjectTagMap>();
            this.Tests = new HashSet<TestSubjectTagMap>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Name { get; set; }

        public virtual ICollection<QuestionCopy> Questions { get; set; }

        public virtual ICollection<UserSubjectTagMap> Users { get; set; }

        public virtual ICollection<TestSubjectTagMap> Tests { get; set; }
    }
}
