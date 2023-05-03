namespace TestPlatform.Database.Entities.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;

    public class Answer : BaseEntity
    {
        public Answer()
        {
            this.Questions = new HashSet<QuestionAnswerMap>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_SIXTEEN, MinimumLength = Validations.ONE)]
        public string Content { get; set; }

        public virtual ICollection<QuestionAnswerMap> Questions { get; set; }
    }
}
