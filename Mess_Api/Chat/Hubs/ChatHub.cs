using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;

namespace Mess_Api.Chat.Hubs
{
    public sealed class ChatHub : Hub<IChatClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(string messageToSend)
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} {messageToSend}");
        }
    }
}

//MUST HAVE SYMBOL IN THE END {"protocol":"json","version":1}
// {"arguments":["Test Message"], "invocationId":"0", "target":"SendMessage", "type":1}

/*
WITHOUT ICHATCLIENT
 public async Task SendMessage(string messageToSend)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} {messageToSend}");
        }
*/
