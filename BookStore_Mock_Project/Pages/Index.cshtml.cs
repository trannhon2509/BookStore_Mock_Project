using BookStore_Mock_Project.Model;
using BookStore_Mock_Project.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;
        private readonly SessionService _session;
        [ActivatorUtilitiesConstructor]
        public IndexModel(BookStore_Mock_Project.Data.ApplicationDbContext context, SessionService session)
        {
            _context = context;
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public IList<Book> Book { get; set; } = default!;
        public IList<BookCategory> Category { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books
                .Include(b => b.Category).ToListAsync();
            }
            if (_context.BookCategories != null)
            {
                Category = await _context.BookCategories
                .ToListAsync();
            }
        }
        public IActionResult OnPostSignOut()
        {
            Console.WriteLine("SignOut");

            // Xóa session
            _session.RemoveSession("UserSessionKey");

            // Đăng xuất người dùng khỏi hệ thống
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect về trang chính
            return Redirect("/Index");
        }
    }
}
