namespace TestPlatform.DTOs.BindingModels.Rooms
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateChatMessageBM : IMapTo<ChatMessage>
    {
        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Message { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoomId { get; set; }
    }
}
