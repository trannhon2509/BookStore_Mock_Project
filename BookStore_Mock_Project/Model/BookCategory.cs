using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Model
{
    public class BookCategory
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Status { get; set; } = true;

        public List<Book> Books { get; set; }
    }
}
