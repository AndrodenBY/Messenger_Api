using Mess_Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Mess_Api.DTO.User
{
    //public record UserDto (Guid? Id, string? Login, string? Email, string? Password);
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        public UserDto(Guid? id, string? login, string? email, string? password)
        {
            Id = id;
            Login = login;
            Email = email;
            PasswordHash = password;
        }
    }
}
