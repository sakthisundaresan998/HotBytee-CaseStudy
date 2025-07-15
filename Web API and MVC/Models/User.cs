namespace HotBytee.MVC.Models
{
    public class User
    {
        public int UserId { get; set; }   // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Consider encrypting in DB
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }
}
