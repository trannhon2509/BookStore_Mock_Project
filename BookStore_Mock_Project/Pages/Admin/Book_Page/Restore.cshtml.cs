using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages.Admin.Book_Page
{
    public class RestoreModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public RestoreModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                Book = book;
                Book.Status = true;
                _context.Attach(Book).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Notify"] = "Delete " + Book.Title + " successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(Book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Notify"] = Book.Title + " has been restored!";
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Index");
        }
        private bool BookExists(Guid id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
