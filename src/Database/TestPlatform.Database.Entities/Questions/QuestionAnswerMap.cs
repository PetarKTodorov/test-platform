namespace TestPlatform.Database.Entities.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class QuestionAnswerMap : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public virtual QuestionCopy Question { get; set; }

        public Guid AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        [Required]
        public bool? IsCorrect { get; set; }
    }
}
