namespace Mess_Api.Chat.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string messageToSend);
    }
}
