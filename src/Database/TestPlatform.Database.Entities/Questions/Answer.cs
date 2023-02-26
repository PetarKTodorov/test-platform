namespace TestPlatform.Database.Entities.Questions
{
    using System.Collections.Generic;

    public class Answer : BaseEntity
    {
        public Answer()
        {
            this.Questions = new HashSet<QuestionAnswerMap>();
        }

        public string Content { get; set; }

        public virtual ICollection<QuestionAnswerMap> Questions { get; set; }
    }
}
