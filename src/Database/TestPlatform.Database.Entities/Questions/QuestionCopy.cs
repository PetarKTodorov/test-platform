namespace TestPlatform.Database.Entities.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Database.Entities.Subjects;

    public class QuestionCopy : BaseEntity
    {
        public QuestionCopy()
        {
            this.Tests = new HashSet<QuestionTestMap>();
            this.Answers = new HashSet<QuestionAnswerMap>();
        }

        public Guid OriginalQuestionId { get; set; }
        public virtual Question OriginalQuestion { get; set; }

        public Guid QuestionTypeId { get; set; }
        public virtual QuestionType QuestionType { get; set; }

        public Guid SubjectTagId { get; set; }
        public virtual SubjectTag SubjectTag { get; set; }

        [Required]
        public bool HasRandomizedAnswers { get; set; }

        public int CorrectAnswersCount { get; set; }

        public virtual ICollection<QuestionTestMap> Tests { get; set; }

        public virtual ICollection<QuestionAnswerMap> Answers { get; set; }
    }
}
