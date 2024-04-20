using Mess_Api.DTO.User;
using Mess_Api.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mess_Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthorizationController(AuthService authService) { _authService = authService; }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userToRegister)//UserRegisterDTO
        {
            
            if (_authService.Register(userToRegister) != null) 
            {
                return Ok(_authService.Register(userToRegister));
            }
            return Ok("Error");
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userToLogin)// UserLoginDTO
        {
            if (_authService.Login(userToLogin) != null) 
            {
                return Ok(_authService.Login(userToLogin));
            }
            return Ok("Error");
        }

    }
}
