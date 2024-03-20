using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; } = true;

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public List<Order> Orders { get; set; }
    }
}
