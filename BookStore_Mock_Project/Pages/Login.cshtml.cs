using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Mock_Project.Pages
{
    public class LoginModel : PageModel
    {

        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }
        public LoginModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task OnGetAsync()
        {

        }
        public IActionResult OnPostLogin()
        {
            var find = _context.Users.Include(u => u.Role)
                              .FirstOrDefault(x => x.Username == username && x.Password == password);
            if (find != null)
            {
                if (find.Role.Name.Equals("Admin"))
                {
                    TempData["admin"] = find.Username;
                    return Redirect("/Admin/Books_Page/Index");
                }
                else
                {
                    TempData["user"] = find.Username;
                    return Redirect("/Index");
                }
            }
            return Redirect("/Index");
        }
        public IActionResult OnPostRegister()
        {
            // Thực hiện đăng ký người dùng ở đây
            // Sau khi đăng ký thành công, bạn có thể đăng nhập người dùng tự động hoặc chuyển hướng đến trang đăng nhập

            // Ví dụ:
            // RegisterUser(Username, Password);

            // Sau khi đăng ký thành công, chuyển hướng đến trang đăng nhập
            return RedirectToPage("/Login");
        }
    }
}
