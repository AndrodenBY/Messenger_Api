using Mess_Api.DTO.Response;
using Mess_Api.DTO.User;
using Mess_Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Mess_Api.Services.Auth
{
    public interface IAuthService
    {
        RegisterResponseDto Register(UserRegisterDto userToRegister);
        string Login(UserLoginDto userToLogin);
    }
}
