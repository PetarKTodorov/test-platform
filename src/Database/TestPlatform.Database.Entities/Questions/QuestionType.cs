namespace TestPlatform.Database.Entities.Questions
{
    using System.Collections.Generic;

    public class QuestionType : BaseEntity
    {
        public QuestionType()
        {
            this.Questions = new HashSet<QuestionCopy>();
        }

        public string Name { get; set; }

        public virtual ICollection<QuestionCopy> Questions { get; set; }
    }
}
