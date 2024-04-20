using Mess_Api.DTO.Response;
using Mess_Api.DTO.User;
using Mess_Api.Models;
using Mess_Api.Repositories.Auth;
using Mess_Api.Services.Jwt;
using Mess_Api.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Mess_Api.Services.Auth
{
    public class AuthService: IAuthService
    {                                 
        private readonly AuthRepository _authRepository;
        private readonly JwtService _jwtService;

        public AuthService(AuthRepository authRepository, JwtService jwtService)
        { _authRepository = authRepository; _jwtService = jwtService; }        

        public RegisterResponseDto Register(UserRegisterDto userToRegister)
        {
            bool isUserRegistered = _authRepository.Register(userToRegister);
            if (isUserRegistered == true)
            {
                UserDto user = new UserDto(null, userToRegister.Login, userToRegister.Email, BCrypt.Net.BCrypt.HashPassword(userToRegister.OriginalPassword));
                string token = _jwtService.GenerateJwtToken(user);
                return new RegisterResponseDto { User = user, Token = token };
            }
            return null;            
        }

        public string Login(UserLoginDto userToLogin)
        {
            bool isUserLogged = _authRepository.Login(userToLogin);
            if(isUserLogged = true)
            {
                UserDto user = new UserDto(null, userToLogin.Login, userToLogin.Email, BCrypt.Net.BCrypt.HashPassword(userToLogin.Password));
                return _jwtService.GenerateJwtToken(user);                
            }
            return null;
        }
    }
}
