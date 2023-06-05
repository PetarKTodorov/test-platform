namespace TestPlatform.Database.Entities.Rooms
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Authorization;

    public class ChatMessage : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Message { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
