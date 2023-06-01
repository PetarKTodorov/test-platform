namespace TestPlatform.DTOs.ViewModels.Rooms
{
    using System.ComponentModel;

    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsRoomVM : IMapFrom<Room>
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Start Date Time")]
        public DateTime StartDateTime { get; set; }

        [DisplayName("End Date Time")]
        public DateTime EndDateTime { get; set; }

        public IEnumerable<RoomParticipantMapVM> Participants { get; set; }
    }
}
