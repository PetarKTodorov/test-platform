namespace TestPlatform.DTOs.ViewModels.Questions
{
    using System;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class QuestionTestPointsVM : IMapFrom<QuestionTestMap>
    {
        public Guid TestId { get; set; }

        public int Points { get; set; }
    }
}
