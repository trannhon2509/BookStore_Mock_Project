using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore_Mock_Project.Data;
using BookStore_Mock_Project.Model;

namespace BookStore_Mock_Project.Pages.Admin.Category_Page
{
    public class DeleteModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public DeleteModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookCategory BookCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            var bookcategory = await _context.BookCategories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (bookcategory == null)
            {
                return NotFound();
            }
            else 
            {
                BookCategory = bookcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }
            var bookcategory = await _context.BookCategories.FindAsync(id);

            if (bookcategory != null)
            {
                BookCategory = bookcategory;
                BookCategory.Status = false;
                _context.Attach(BookCategory).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Notify"] = "Delete " + BookCategory.Name + " successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCategoryExists(BookCategory.CategoryId))
                    {
                        TempData["Notify"] = BookCategory.Name + " is deleted fail.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Notify"] = BookCategory.Name + " is successfully deleted!";
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
        private bool BookCategoryExists(Guid id)
        {
            return (_context.BookCategories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
