using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore_Mock_Project.Data;
using BookStore_Mock_Project.Model;

namespace BookStore_Mock_Project.Pages.Admin.Book_Page
{
    public class DeleteModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public DeleteModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
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
                Book.Status = false;
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
                        TempData["Notify"] = Book.Title + " is deleted fail.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Notify"] = Book.Title + " is successfully deleted!";
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
