namespace api.Models
{
    public class Login
    {
        //either 200 or 400 
        public int Response { get; set; }
        public string? Message { get; set; }
        public string? Username { get; set; }
        public AuthToken? AuthToken { get; set; }
    }
}