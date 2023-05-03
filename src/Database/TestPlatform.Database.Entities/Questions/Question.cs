namespace TestPlatform.Database.Entities.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;

    public class Question : BaseEntity
    {
        public Question()
        {
            this.Copies = new HashSet<QuestionCopy>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_SIXTEEN, MinimumLength = Validations.ONE)]
        public string Title { get; set; }

        public virtual ICollection<QuestionCopy> Copies { get; set; }
    }
}
