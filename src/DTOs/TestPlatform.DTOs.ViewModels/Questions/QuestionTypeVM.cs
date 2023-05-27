﻿namespace TestPlatform.DTOs.ViewModels.Questions
{
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class QuestionTypeVM : IMapFrom<QuestionType>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
