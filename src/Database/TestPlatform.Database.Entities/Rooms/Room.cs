namespace TestPlatform.Database.Entities.Rooms
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TestPlatform.Database.Entities.Tests;

    public class Room : BaseEntity
    {
        public Room()
        {
            this.Participants = new HashSet<RoomParticipantMap>();
        }

        // TODO: Make a DateComparisonValidationAttribute to stop end date be before start date
        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }

        [NotMapped]
        public int ParticipantsCount => this.Participants.Count();

        public virtual ICollection<RoomParticipantMap> Participants { get; set; }
    }
}
