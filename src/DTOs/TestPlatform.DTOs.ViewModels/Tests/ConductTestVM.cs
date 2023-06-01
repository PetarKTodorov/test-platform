namespace TestPlatform.DTOs.ViewModels.Tests
{
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.DTOs.ViewModels.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ConductTestVM : IMapFrom<Room>
    {
        public Guid Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }

        public string TestTitle { get; set; }

        public string TestInstructions { get; set; }

        public IList<DetailsQuestionTestVM> TestQuestions { get; set; }
    }
}
