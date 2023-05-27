namespace TestPlatform.DTOs.ViewModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsQuestionCopyVM : IMapFrom<QuestionCopy>
    {
        public Guid Id { get; set; }

        public string OriginalQuestionTitle { get; set; }

        public bool HasRandomizedAnswers { get; set; }

        public string QuestionTypeName { get; set; }

        [Required]
        public string SubjectTagName { get; set; }
    }
}
