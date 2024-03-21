using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages
{
    public class CardModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public IList<Book> Cart { get; private set; }

        public CardModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            // Lấy danh sách GUID của các sách từ TempData
            var cartItems = TempData["Cart"] as List<Guid> ?? new List<Guid>();

            // Truy vấn cơ sở dữ liệu để lấy thông tin chi tiết của từng sách
            Cart = await _context.Books
                .Include(b => b.Category)
                .Where(b => cartItems.Contains(b.BookId))
                .ToListAsync();
        }
    }
}
