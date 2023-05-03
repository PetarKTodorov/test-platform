namespace TestPlatform.Database.Entities.Rooms
{
    using System;

    using TestPlatform.Database.Entities.Authorization;

    public class RoomParticipantMap : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
