namespace TestPlatform.DTOs.BindingModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DeleteQuestionAnswerMap : IMapTo<QuestionAnswerMap>
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }
    }
}
