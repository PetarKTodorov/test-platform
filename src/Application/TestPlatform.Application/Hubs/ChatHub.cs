namespace TestPlatform.Application.Hubs
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.SignalR;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Common.Constants;
    using TestPlatform.DTOs.BindingModels.Rooms;
    using TestPlatform.DTOs.ViewModels.Rooms;
    using TestPlatform.Services.Database.Rooms.Interfaces;

    [CustomAuthorize(ApplicationRoles.STUDENT)]
    public class ChatHub : Hub
    {
        private readonly IChatMessageService chatMessageService;

        public ChatHub(IChatMessageService chatMessageService)
        {
            this.chatMessageService = chatMessageService;
        }

        public async Task SendMessageAsync(string userEmail, string message)
        {
            var roomId = this.Context.GetHttpContext().Request.Query["roomId"];
            var currentUserId = Guid.Parse(this.Context.User.FindFirstValue(UserClaimTypes.ID));

            var createChatMessageBM = new CreateChatMessageBM()
            {
                Message = message,
                RoomId = Guid.Parse(roomId),
                UserId = currentUserId
            };

            var newChatMessage = await this.chatMessageService
                .CreateAsync<ChatMessageVM, CreateChatMessageBM>(createChatMessageBM, currentUserId);

            var messageTime = newChatMessage.CreatedDate.ToString("HH:mm");
            var isCurrentUser = userEmail == this.Context.User.FindFirstValue(UserClaimTypes.EMAIL);

            await this.Clients.Group(roomId).SendAsync("ReceiveMessageAsync", userEmail, message, messageTime, isCurrentUser);
        }

        public override async Task OnConnectedAsync()
        {
            var roomId = this.Context.GetHttpContext().Request.Query["roomId"];
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, roomId);

            var chatMessages = await this.chatMessageService
                .FindByRoomIdAsync<ChatMessageVM>(Guid.Parse(roomId));

            foreach (var chatMessage in chatMessages)
            {
                var messageTime = chatMessage.CreatedDate.ToString("HH:mm");
                var isCurrentUser = chatMessage.UserEmail == this.Context.User.FindFirstValue(UserClaimTypes.EMAIL);

                await this.Clients.Client(this.Context.ConnectionId)
                    .SendAsync("ReceiveMessageAsync", chatMessage.UserEmail, chatMessage.Message, messageTime, isCurrentUser);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var roomId = this.Context.GetHttpContext().Request.Query["roomId"];
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, roomId.ToString());

            await base.OnDisconnectedAsync(exception);
        }
    }
}
