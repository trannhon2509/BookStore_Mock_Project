using BookStore_Mock_Project.Model;
using BookStore_Mock_Project.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_Mock_Project.Pages
{
    public class LoginModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;
        private BookStore_Mock_Project.Model.Role[] _roles;
        private readonly SessionService _session;
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string email { get; set; }
        public LoginModel(BookStore_Mock_Project.Data.ApplicationDbContext context, SessionService session)
        {
            _context = context;
            _roles = _context.Roles.ToArray();
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public async Task OnGetAsync()
        {
            Console.WriteLine("Login Page is display");
        }

        public IActionResult OnPostSignIn()
        {
            User? account = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (account != null)
            {
                if (_session != null)
                {
                    _session.Set<User>("Info", account);
                    return Redirect("/Index");
                }
                return Page();
            }
            Console.WriteLine("Wrong username and password.");
            return Page();
        }
        public IActionResult OnPostSignUp()
        {
            var account = _context.Users.Where(u => u.Username == username.Trim() || u.Email == email.Trim());
            Role? role = _context.Roles.FirstOrDefault(r => r.Name == "User");
            if (account.Count() == 0 && role != null)
            {
                User user = new User();
                user.Username = username.Trim();
                user.Password = password.Trim();
                user.Email = email.Trim();
                user.Status = true;
                user.UserId = Guid.NewGuid();
                user.RoleId = role.RoleId;
                _context.Users.Add(user);
                _context.SaveChanges();
                Console.WriteLine("Register successfully!");
            }
            else
            {
                Console.WriteLine("Fail to register!");
            }
            return Redirect("/Login");
        }

        public IActionResult OnPostLogin()
        {
            Console.WriteLine("On post login is running");
            return Redirect("/Index");
        }
        public IActionResult OnPostRegister()
        {
            Console.WriteLine("On post register is running");
            return RedirectToPage("/Login");
        }
        public IActionResult OnPostSignOut()
        {
            Console.WriteLine("SignOut");


            return Redirect("/Index");
        }

        public IActionResult OnPostForget()
        {
            /*
             _from, _gmailsend: Email được gửi đi
            _to: Email đến người cần gửi
            _subject: Tiêu đề của email
            _body: Nội dung của email
            _gmailpassword: Mật khẩu mà chúng ta đã tạo ở bước 1
             */
            User? account = _context.Users.FirstOrDefault(r => r.Username == username);

            if (account != null)
            {
                string _from = "nhonvn2509@gmail.com";
                string _to = account.Email;
                string _subject = "Reset password";
                string _body = "Mật khẩu của bạn là: " + account.Password;
                string _gmailsend = "nhonvn2509@gmail.com";
                string _gmailpassword = "tjklvkkfamkyzcrm";
                MailUltil.SendMailGoogleSmtp(_from, _to, _subject, _body, _gmailsend, _gmailpassword);
                Console.WriteLine("Send mail successfully!");
                return Redirect("Login");
            }
            Console.WriteLine("Not found username");
            return Redirect("/Login");
        }


    }
}
