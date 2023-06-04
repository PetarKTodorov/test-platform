namespace TestPlatform.Database.Entities.Rooms
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TestPlatform.Common.Attributes;
    using TestPlatform.Database.Entities.Tests;

    public class Room : BaseEntity
    {
        public Room()
        {
            this.Participants = new HashSet<RoomParticipantMap>();
        }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DateComparison(nameof(StartDateTime))]
        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }

        public Guid? ChatConnectionId { get; set; }

        public virtual ChatConnection ChatConnetion { get; set; }

        [NotMapped]
        public int ParticipantsCount => this.Participants.Count();

        public virtual ICollection<RoomParticipantMap> Participants { get; set; }
    }
}
