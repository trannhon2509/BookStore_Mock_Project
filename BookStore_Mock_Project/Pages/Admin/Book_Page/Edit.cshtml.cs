using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore_Mock_Project.Data;
using BookStore_Mock_Project.Model;
using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Pages.Admin.Book_Page
{
    public class EditModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;
        IHostEnvironment _environment;

        public EditModel(BookStore_Mock_Project.Data.ApplicationDbContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile? FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book =  await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
           ViewData["CategoryId"] = new SelectList(_context.BookCategories, "CategoryId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (FileUpload != null)
            {
                var file = Path.Combine(_environment.ContentRootPath, "Pages\\image", FileUpload.FileName);
                Console.WriteLine(FileUpload.FileName);
                Book.image = FileUpload.FileName;
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);
                }
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.BookId))
                {
                    TempData["Notify"] = Book.Title + " is updated fail!";
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["Notify"] = Book.Title + " is updated successfully";
            return RedirectToPage("./Index");
        }

        private bool BookExists(Guid id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
