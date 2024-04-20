using Mess_Api.DTO.User;
using Mess_Api.Repositories.User;

namespace Mess_Api.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository) { _repository = repository; }

        public UserDto Create(UserDto userToCreate)
        {
            bool isUserCreated = _repository.Create(userToCreate);
            if (isUserCreated)
            {
                return userToCreate;
            }
            return null;
        }

        public UserDto GetUserById(Guid? userId)
        {
            
            if (_repository.GetUserById(userId) != null) 
            {
                return _repository.GetUserById(userId);
            }
            return null;
        }

        public UserDto GetUserByLogin(string userLogin)
        {
            if (_repository.GetUserByEmail(userLogin) != null)
            {
                return _repository.GetUserByEmail(userLogin);
            }
            return null;
        }

        public UserDto GetUserByEmail(string userEmail) 
        {
            if(_repository.GetUserByEmail(userEmail) != null)
            {
                return _repository.GetUserByEmail(userEmail);
            }
            return null;
        }

        public UserDto GetUserByPasswordHash(string userPassword)
        {
            if (_repository.GetUserByPasswordHash(userPassword) != null)
            {
                return _repository.GetUserByPasswordHash(userPassword);
            }
            return null;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public bool PasswordConfirmed(string originalPassword, string passwordEntered)
        {
            bool isPasswordConfirmed = _repository.PasswordConfirmed(originalPassword ,passwordEntered);
            return isPasswordConfirmed;                       
        }
    }
}
