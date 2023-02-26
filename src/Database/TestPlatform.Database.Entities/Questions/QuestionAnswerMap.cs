namespace TestPlatform.Database.Entities.Questions
{
    using System;

    public class QuestionAnswerMap : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public virtual QuestionCopy Question { get; set; }

        public Guid AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        public bool? IsCorrect { get; set; }
    }
}
