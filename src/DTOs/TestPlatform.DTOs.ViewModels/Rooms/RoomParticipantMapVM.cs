namespace TestPlatform.DTOs.ViewModels.Rooms
{
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class RoomParticipantMapVM : IMapFrom<RoomParticipantMap>
    {
        public string UserEmail { get; set; }
    }
}
