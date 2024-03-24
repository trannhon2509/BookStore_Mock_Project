using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Model
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailId { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public Guid BookId { get; set; }
        public Book? Book { get; set; }
    }
}
