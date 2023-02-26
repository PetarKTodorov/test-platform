namespace TestPlatform.Database.Entities.Rooms
{
    using System;

    using TestPlatform.Database.Entities.Tests;

    public class Room : BaseEntity
    {
        public Room()
        {
            this.Participants = new HashSet<RoomParticipantMap>();
        }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }

        // TODO: Ignor it to database
        public int ParticipantsCount => this.Participants.Count();

        public virtual ICollection<RoomParticipantMap> Participants { get; set; }
    }
}
