using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mess_Api.Authorization
{
    public class AuthOptions
    {
        public const string ISSUER = "MessengerApi"; // издатель токена
        public const string AUDIENCE = "localhost:7219"; // потребитель токена
        const string KEY = "HAEJwroDj7yRjdd3MfbLclg1BnN7lmK7";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
