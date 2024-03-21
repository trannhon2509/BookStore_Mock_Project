using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages.Admin.Categories_Page
{
    public class IndexModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public IndexModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BookCategory> BookCategory { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BookCategories != null)
            {
                TempData["admin"] = "admin";
                BookCategory = await _context.BookCategories.ToListAsync();
            }
        }
    }
}
