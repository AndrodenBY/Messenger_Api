using Mess_Api.DTO.User;

namespace Mess_Api.Repositories.User
{
    public interface IUserRepository
    {
        bool Create(UserDto userToCreate);
        UserDto GetUserById(Guid? userId);
        UserDto GetUserByLogin(string userLogin);
        UserDto GetUserByEmail(string userEmail);
        UserDto GetUserByPasswordHash(string passwordHash);
        IEnumerable<UserDto> GetAllUsers();
        bool PasswordConfirmed(string originalPassword, string enteredPassword);
        bool IsAuthenticated(string? password, string? passwordHash);
        
    }
}
