namespace TestPlatform.DTOs.BindingModels.Rooms
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateRoomParticipantMapBM : IMapTo<RoomParticipantMap>, IMapFrom<RoomParticipantMap>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoomId { get; set; }
    }
}
