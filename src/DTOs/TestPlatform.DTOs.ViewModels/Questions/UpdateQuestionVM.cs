namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System.ComponentModel;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.DTOs.ViewModels.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateQuestionVM : IMapFrom<QuestionCopy>
    {
        public Guid Id { get; set; }

        [DisplayName("Title")]
        public string OriginalQuestionTitle { get; set; }

        [DisplayName("Has Randomized Answers")]
        public bool HasRandomizedAnswers { get; set; }

        [DisplayName("Question Type")]
        public string QuestionTypeId { get; set; }

        [DisplayName("Subject Tag")]
        public string SubjectTagId { get; set; }

        public List<SelectListItem> QuestionTypes { get; set; }

        public List<SelectListItem> SubjectTags { get; set; }
    }
}
