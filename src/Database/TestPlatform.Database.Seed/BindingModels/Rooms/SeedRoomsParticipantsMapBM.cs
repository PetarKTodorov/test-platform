namespace TestPlatform.Database.Seed.BindingModels.Rooms
{
    using System;

    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedRoomsParticipantsMapBM : IMapTo<RoomParticipantMap>
    {
        public Guid Id { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid UserId { get; set; }

        public Guid RoomId { get; set; }
    }
}
