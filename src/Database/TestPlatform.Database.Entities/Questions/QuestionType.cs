namespace TestPlatform.Database.Entities.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;

    public class QuestionType : BaseEntity
    {
        public QuestionType()
        {
            this.Questions = new HashSet<QuestionCopy>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Name { get; set; }

        public virtual ICollection<QuestionCopy> Questions { get; set; }
    }
}
