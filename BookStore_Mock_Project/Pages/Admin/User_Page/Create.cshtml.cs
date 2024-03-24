using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore_Mock_Project.Pages.Admin.User_Page
{
    public class CreateModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public CreateModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            User.Status = true;
            if (_context.Users == null || User == null)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            TempData["Notify"] = "Create " + User.Username + " successfully!";
            return RedirectToPage("./Index");
        }
    }
}
