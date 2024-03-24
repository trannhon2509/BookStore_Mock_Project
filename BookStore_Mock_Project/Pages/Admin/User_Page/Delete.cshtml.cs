using BookStore_Mock_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages.Admin.User_Page
{
    public class DeleteModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public DeleteModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                User = user;
                User.Status = false;
                _context.Attach(User).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Notify"] = "Delete " + User.Username + " successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(User.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
