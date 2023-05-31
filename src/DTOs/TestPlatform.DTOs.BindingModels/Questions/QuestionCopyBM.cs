namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Mapper.Interfaces;

    public class QuestionCopyBM : BaseBM, IMapFrom<QuestionCopy>
    {

        public Guid SubjectTagId { get; set; }
    }
}
