using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages.Admin.Books_Page
{
    public class IndexModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public IndexModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books
                .Include(b => b.Category).ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync(Guid bookId)
        {
            var bookToUpdate = _context.Books.FirstOrDefault(b => b.BookId == bookId);

            if (bookToUpdate != null)
            {
                // Cập nhật trạng thái
                bookToUpdate.Status = !bookToUpdate.Status;

                // Lưu thay đổi vào CSDL
                _context.SaveChanges();

                TempData["RestoreBook"] = bookToUpdate.Title + " restored successfully";

                return RedirectToPage("./Index"); // Hoặc bạn có thể chuyển hướng đến trang khác nếu cần
            }

            return RedirectToPage();
        }
    }
}
