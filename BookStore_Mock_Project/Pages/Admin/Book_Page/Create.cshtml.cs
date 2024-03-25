using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace BookStore_Mock_Project.Pages.Admin.Book_Page
{
    public class CreateModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;
        private IHostEnvironment _enviroment;
        private IHubContext<SignalServer> _signalRHub;

        public CreateModel(BookStore_Mock_Project.Data.ApplicationDbContext context, IHostEnvironment enviroment, IHubContext<SignalServer> signalRHub)
        {
            _context = context;
            _enviroment = enviroment;
            _signalRHub = signalRHub;
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
            await _signalRHub.Clients.All.SendAsync("LoadBooks");
            TempData["Notify"] = "A new book called " + Book.Title + " is on the list!";

            return RedirectToPage("./Index");
        }
    }
}
