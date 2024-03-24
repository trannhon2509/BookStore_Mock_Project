using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Model
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Status { get; set; } = true;
        [JsonIgnore]
        public List<User>? Users { get; set; }
    }
}
