namespace TestPlatform.DTOs.ViewModels.Rooms
{
    using System.ComponentModel;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ListRoomsVM : IMapFrom<Room>
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public bool IsDeleted { get; set; }

        [DisplayName("Start Date Time")]
        public DateTime StartDateTime { get; set; }

        [DisplayName("End Date Time")]
        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }

        [DisplayName("Test Title")]
        public string TestTitle { get; set; }
    }
}
