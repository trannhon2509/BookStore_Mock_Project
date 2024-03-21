using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages.Admin.Users_Page
{
    public class IndexModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public IndexModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                TempData["admin"] = "admin";
                User = await _context.Users
                .Include(u => u.Role).ToListAsync();
            }
        }
    }
}
