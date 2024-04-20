using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Mess_Api.Chat.Hubs;
using Mess_Api.Chat;

namespace Mess_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;
        
        public ChatController(IHubContext<ChatHub, IChatClient> hubContext)
        {
            _hubContext = hubContext;            
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] string messageToSend)
        {
            //await _hubContext.Clients.All.ReceiveMessage($"{Context.ConnectionId} {messageToSend}");
            return Ok();
        }

        [HttpGet("OnConnected")]
        public async Task<IActionResult> OnConnected()
        {
            //await _hubContext.Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
            return Ok();
        }

        [Authorize]
        [HttpPost("SendNewMessage")]
        public async Task<IActionResult> SendNewMessage([FromBody] string chatMessage)
        {
            var login = User.Identity.Name; // Получаем логин пользователя из токена

            // Отправка сообщения, например через SignalR Hub
            await _hubContext.Clients.All.ReceiveMessage(chatMessage.ToString());

            return Ok(new { Login = login});
        }
    }
}
