namespace Mess_Api.DTO.User
{
    public class UserRegisterDto
    {
        public string? Login { get; set; }        
        public string? Email { get; set; }
        public string? OriginalPassword { get; set; }
        public string? EnteredPassword { get; set; }
    }
}
