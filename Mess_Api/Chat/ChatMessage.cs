using Mess_Api.DTO.User;

namespace Mess_Api.Chat
{
    public class ChatMessage
    {
        public UserDto Login { get; set; }
        public string Message { get; set; }
    }
}
