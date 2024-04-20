using Mess_Api.DTO.User;

namespace Mess_Api.Repositories.Auth
{
    public interface IAuthRepository
    {
        bool Login(UserLoginDto userToLogin);
        bool Register(UserRegisterDto userToRegister);
    }
}
