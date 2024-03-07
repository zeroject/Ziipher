using Microsoft.AspNetCore.SignalR;

namespace DirectMessageAPI;

public class ChatHub : Hub
{

    public async Task SendMessage(string user, string message, string room)
    {
        await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinChat(string room)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, room);
    }

    public async Task LeaveChat(string room)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
    }
}
