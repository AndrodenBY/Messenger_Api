namespace Mess_Api.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateJwtToken(string token);
    }
}
