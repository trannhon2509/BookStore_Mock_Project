namespace BookStore_Mock_Project.Model
{
    public class OrderedBook
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public int Quantity { get; set; }
    }
}
