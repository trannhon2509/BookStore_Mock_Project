using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore_Mock_Project.Data;
using BookStore_Mock_Project.Model;
using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Pages.Admin.Book_Page
{
    public class CreateModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;
        private IHostEnvironment _enviroment;

        public CreateModel(BookStore_Mock_Project.Data.ApplicationDbContext context, IHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.BookCategories, "CategoryId", "Name");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile? FileUpload { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (FileUpload != null)
            {
                var file = Path.Combine(_enviroment.ContentRootPath, "Pages\\image", FileUpload.FileName);
                Console.WriteLine(FileUpload.FileName);
                Book.image = FileUpload.FileName;
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);
                }
            }
            else
            {
                Book.image = "image/No_Image_Available.jpg";
            }
            if (!ModelState.IsValid || _context.Books == null || Book == null)
            {
                return OnGet();
            }
            Book.Status = true;
            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            TempData["Notify"] = "A new book called " + Book.Title + " is on the list!";

            return RedirectToPage("./Index");
        }
    }
}
