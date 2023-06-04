namespace TestPlatform.Application.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Common.Constants;

    [CustomAuthorize(ApplicationRoles.STUDENT)]
    public class ChatHub : Hub
    {
        public ChatHub()
        {
        }

        public async Task SendMessageAsync(string user, string message)
        {
            var roomId = this.Context.GetHttpContext().Request.Query["roomId"];
            await this.Clients.Group(roomId).SendAsync("ReceiveMessageAsync", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var roomId = this.Context.GetHttpContext().Request.Query["roomId"];
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, roomId);

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
