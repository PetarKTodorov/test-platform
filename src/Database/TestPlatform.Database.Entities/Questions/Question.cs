namespace TestPlatform.Database.Entities.Questions
{
    using System.Collections.Generic;

    public class Question : BaseEntity
    {
        public Question()
        {
            this.Copies = new HashSet<QuestionCopy>();
        }

        public string Title { get; set; }

        public virtual ICollection<QuestionCopy> Copies { get; set; }
    }
}
