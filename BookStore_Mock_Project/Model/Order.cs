using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Model
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; } = true;

        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
