namespace TestPlatform.DTOs.ViewModels.Rooms
{
    using System.ComponentModel;

    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.DTOs.ViewModels.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsRoomVM : IMapFrom<Room>
    {
        public Guid Id { get; set; }

        [DisplayName("Test Title")]
        public string TestTitle { get; set; }

        public Guid TestId { get; set; }

        public bool IsDeleted { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Start Date Time")]
        public DateTime StartDateTime { get; set; }

        [DisplayName("End Date Time")]
        public DateTime EndDateTime { get; set; }

        [DisplayName("User Grades")]
        public IEnumerable<TestUserVM> TestUsers { get; set; }

        public IEnumerable<RoomParticipantMapVM> Participants { get; set; }
    }
}
