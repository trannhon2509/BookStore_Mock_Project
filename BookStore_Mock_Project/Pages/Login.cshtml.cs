using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages
{
    public class LoginModel : PageModel
    {

        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public LoginModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task OnGetAsync()
        {

        }
        public IActionResult OnPost()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            var find = _context.Users.Include(u => u.Role)
                              .FirstOrDefault(x => x.Username == username && x.Password == password);

            if (find != null)
            {
                if (find.Role.Name.Equals("Admin"))
                {
                    TempData["admin"] = find.Username;
                    return Redirect("/Index");
                }
                else
                {
                    TempData["user"] = find.Username;
                    return Redirect("/Index");
                }
            }
            return Page();
        }
    }
}
