using BCrypt.Net;
using Mess_Api;
using Mess_Api.DTO.User;
using Mess_Api.Models;
using Serilog;

namespace Mess_Api.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly MessApiContext _context;
        public UserRepository(MessApiContext context) { _context = context; }

        public bool Create(UserDto userToCreate)
        {
            Models.User newUser = new Models.User
            {
                Id = userToCreate.Id,
                Login = userToCreate.Login,
                Email = userToCreate.Email,                
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userToCreate.PasswordHash)
            };
            try
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred in Create method: {ex.Message}");
                return false;
            }
        }

        public UserDto GetUserById(Guid? userId)
        {
            try
            {
                return _context.Users.Select(x => new UserDto(x.Id, x.Login, x.Email, null)).FirstOrDefault(x => x.Id == userId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred in GetUserById method: {ex.Message}");
                return null;
            }
        }

        public UserDto GetUserByLogin(string userLogin)
        {
            try
            {
                return _context.Users.Select(x => new UserDto(x.Id, x.Login, x.Email, null)).FirstOrDefault(x => x.Login == userLogin);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred in GetUserByLogin method: {ex.Message}");
                return null;
            }
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            try
            {
                return _context.Users.Select(x => new UserDto(x.Id, x.Login, x.Email, null)).FirstOrDefault(x => x.Email == userEmail);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred in GetUserByEmail method: {ex.Message}");
                return null;
            }
        }

        public UserDto GetUserByPasswordHash(string passwordToHash)
        {
            try
            {
                string enteredPassword = BCrypt.Net.BCrypt.HashPassword(passwordToHash);
                return _context.Users.Select(x => new UserDto(x.Id, x.Login, x.Email, null)).FirstOrDefault(x => x.PasswordHash == enteredPassword);
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"An error occurred in GetUserByPasswordHash method: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            try
            {
                return _context.Users.Select(x => new UserDto(x.Id, x.Login, x.Email, null)).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred in GetAllUsers method: {ex.Message}");
                return null;
            }
        }

        public bool PasswordConfirmed(string? originalPassword, string? enteredPassword)
        {
            string newSalt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedOriginalPassword = BCrypt.Net.BCrypt.HashPassword(originalPassword, newSalt);
            string hashedEnteredPassword = BCrypt.Net.BCrypt.HashPassword(enteredPassword, newSalt);

            return BCrypt.Net.BCrypt.Verify(hashedEnteredPassword, hashedOriginalPassword);                        
            //return BCrypt.Net.BCrypt.Verify(enteredPassword, originalPassword);
        }

        bool IUserRepository.IsAuthenticated(string? password, string? passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}
