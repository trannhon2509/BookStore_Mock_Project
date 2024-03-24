namespace BookStore_Mock_Project.Model
{
    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }

        private decimal _SubTotal;
        public decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = Convert.ToDecimal(Book.Price * Quantity); }
        }
    }
}
