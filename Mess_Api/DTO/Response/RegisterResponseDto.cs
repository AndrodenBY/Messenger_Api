using Mess_Api.DTO.User;

namespace Mess_Api.DTO.Response
{
    public class RegisterResponseDto 
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
