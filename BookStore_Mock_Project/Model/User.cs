using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Status { get; set; }
        public string? Email { get; set; }
        [Required]
        public Guid RoleId { get; set; }
        [Required]
        [JsonIgnore]
        public Role Role { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
