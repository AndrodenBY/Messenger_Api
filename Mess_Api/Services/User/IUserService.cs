using Mess_Api.DTO.User;

namespace Mess_Api.Services.User
{
    public interface IUserService
    {
        UserDto Create(UserDto userToCreate);
        UserDto GetUserById(Guid? id);
        UserDto GetUserByLogin(string login);
        UserDto GetUserByEmail(string email);
        UserDto GetUserByPasswordHash(string userPassword);
        IEnumerable<UserDto> GetAllUsers();
        bool PasswordConfirmed(string originalPassword, string passwordEntered);
    }
}
