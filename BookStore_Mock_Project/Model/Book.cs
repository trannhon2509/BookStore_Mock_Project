using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Model
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required decimal Price { get; set; }
        public string? image { get; set; }
        public string? Description { get; set; }
        public required int Quantity { get; set; }
        public bool Status { get; set; } = true;
        public Guid? CategoryId { get; set; }
        public BookCategory? Category { get; set; }


    }
}
