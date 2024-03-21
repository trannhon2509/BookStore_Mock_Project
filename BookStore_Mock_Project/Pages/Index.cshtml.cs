using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public IndexModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = new List<Book>();

        // Danh sách giỏ hàng
        public IList<Book> CartItems { get; set; }

        public async Task OnGetAsync()
        {
            Book = await _context.Books
                .Include(b => b.Category)
                .ToListAsync();
        }

        public IActionResult OnPostAddToCart()
        {

            // Lấy ID của sách từ form POST
            if (Guid.TryParse(Request.Form["bookId"], out Guid bookId))
            {
                // Lấy danh sách GUID của sách từ TempData
                var cartItems = TempData["Cart"] as List<Guid> ?? new List<Guid>();

                // Thêm GUID sách vào danh sách giỏ hàng
                cartItems.Add(bookId);

                // Chuyển đổi danh sách GUID thành chuỗi và lưu trữ trong TempData
                TempData["Cart"] = string.Join(",", cartItems);
                return Redirect("/Card");
            }

            // Chuyển hướng người dùng về trang Index sau khi thêm sách vào giỏ hàng
            return Redirect("/Index");
        }
    }
}
