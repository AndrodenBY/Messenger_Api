using Mess_Api.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mess_Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService) { _userService = userService; }

        [HttpGet("GetById")]
        public IActionResult GetUserById(Guid userId) 
        {
            if(_userService.GetUserById(userId) != null)
            {
                return Ok(_userService.GetUserById(userId));
            }
            return Ok("Error");
        }

        [HttpGet("GetByLogin")]
        public IActionResult GetUserByLogin(string userLogin)
        {
            if (_userService.GetUserByLogin(userLogin) != null)
            {
                return Ok(_userService.GetUserByLogin(userLogin));
            }
            return Ok("Error");
        }

        [HttpGet("GetByAmail")]
        public IActionResult GetUserByEmail(string userEmail)
        {
            if (_userService.GetUserByEmail(userEmail) != null)
            {
                return Ok(_userService.GetUserByEmail(userEmail));
            }
            return Ok("Error");
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }
    }
}
