using Mess_Api.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BCrypt.Net;
using Mess_Api.Models;
using Mess_Api.Services.Jwt;
using Mess_Api.Repositories.User;
using Mess_Api;
using Mess_Api.DTO.User;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Mess_Api.Repositories.Auth
{
    public class AuthRepository: IAuthRepository
    {
        private readonly Services.User.UserService _userService;        
        public AuthRepository(Services.User.UserService userService) { _userService = userService; }

        public bool Login(UserLoginDto userToLogin)
        {
            try
            {
                UserLoginDto loginUser = new UserLoginDto
                { 
                    Login = userToLogin.Login,
                    Email = userToLogin.Email,
                    Password = userToLogin.Password
                };
                //Cделать так, чтобы при выполнении запроса без логина или пароля, в запрос приходила также и недостающая часть
                UserDto userEmail = _userService.GetUserByEmail(userToLogin.Email);
                UserDto userLogin = _userService.GetUserByLogin(userToLogin.Login);
                UserDto userPassword = _userService.GetUserByPasswordHash(userToLogin.Password);                
                if (userEmail == null && userPassword == null || userLogin == null && userPassword == null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred in Login method: {ex.Message}");
                return false;
            }
        }

        public bool Register(UserRegisterDto userToRegister)
        {
            try
            {
                UserRegisterDto registerUser = new UserRegisterDto
                {
                    Login = userToRegister.Login,
                    Email = userToRegister.Email,
                    OriginalPassword = userToRegister.OriginalPassword,
                    EnteredPassword = userToRegister.EnteredPassword,
                };
                if (_userService.GetUserByEmail(registerUser.Email) != null)
                {
                    return false;
                }
                bool confirmedPassword = _userService.PasswordConfirmed(registerUser.OriginalPassword, registerUser.EnteredPassword);
                if (confirmedPassword = false) // КАК ЕЩЕ ЗАПИСАТЬ IF С ПОМОЩЬЮ ? И :
                {
                    return false;
                }
                UserDto user = new UserDto(Guid.NewGuid(), registerUser.Login, registerUser.Email, BCrypt.Net.BCrypt.HashPassword(registerUser.OriginalPassword));
                _userService.Create(user);
                return true;
            }
            catch(Exception ex) 
            {
                Log.Error(ex, $"An error occurred in Register method: {ex.Message}");
                return false; 
            }
        }
    }
}

