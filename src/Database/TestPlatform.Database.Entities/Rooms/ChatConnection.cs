namespace TestPlatform.Database.Entities.Rooms
{
    using System.ComponentModel.DataAnnotations;

    public class ChatConnection : BaseEntity
    {
        // Change it to history and save the messages

        [Required]
        public string ConnectionId { get; set; }

        [Required]
        public bool Connected { get; set; }

        public virtual Room Room { get; set; }
    }
}
