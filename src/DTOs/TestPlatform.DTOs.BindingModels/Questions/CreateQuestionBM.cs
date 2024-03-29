﻿namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateQuestionBM : IMapTo<Question>
    {
        public CreateQuestionBM()
        {
            this.Answers = new List<UpdateQuestionAnswerBM>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Has Randomized Answers")]
        public bool HasRandomizedAnswers { get; set; }

        [Required]
        [DisplayName("Question Type")]
        public Guid? QuestionTypeId { get; set; }

        [Required]
        [DisplayName("Subject Tag")]
        public Guid? SubjectTagId { get; set; }

        public IEnumerable<UpdateQuestionAnswerBM> Answers { get; set; }
    }
}
