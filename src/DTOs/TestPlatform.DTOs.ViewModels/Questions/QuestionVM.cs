namespace TestPlatform.DTOs.ViewModels.Questions
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Mapper.Interfaces;

    public class QuestionVM : BaseBM, IMapFrom<Question>
    {
        public string Title { get; set; }
    }
}
