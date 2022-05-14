namespace grud_backend.Models
{
    public class AuthenticateModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class SetPwModel
    {
        public string oldPassword{get; set;}
        public string newPassword{get; set;}
    }
}