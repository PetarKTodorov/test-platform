namespace TestPlatform.DTOs.ViewModels.Rooms
{
    using System;

    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ChatMessageVM : IMapFrom<ChatMessage>
    {
        public DateTime CreatedDate { get; set; }

        public string Message { get; set; }

        public string UserEmail { get; set; }
    }
}
