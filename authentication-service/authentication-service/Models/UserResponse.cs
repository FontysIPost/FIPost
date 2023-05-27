namespace authentication_service.Models
{
    public class UserResponse
    {
        public string email { get; set; }
        public int role { get; set; }

        public UserResponse(string email, int role) 
        {
            this.email = email;
            this.role = role;
        }
    }
}
