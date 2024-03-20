using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Model
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; } = true;

        public Guid? CategoryId { get; set; }
        public BookCategory? Category { get; set; }
    }
}
