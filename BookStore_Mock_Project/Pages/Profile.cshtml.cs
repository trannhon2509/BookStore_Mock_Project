using BookStore_Mock_Project.Model;
using BookStore_Mock_Project.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_Mock_Project.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        private readonly SessionService _session;
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }
        [ActivatorUtilitiesConstructor]
        public ProfileModel(BookStore_Mock_Project.Data.ApplicationDbContext context, SessionService session)
        {
            _context = context;
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task OnGetAsync()
        {

        }
        public IActionResult OnPostChangeInfo()
        {
            Console.WriteLine("Change infomation is running");
            User userChange = _session.Get<User>("Info");
            User userInput = _context.Users.FirstOrDefault(u => u.Email == userChange.Email && u.Password == userChange.Password);
            if (userInput != null)
            {
                userInput.Email = email;
                userInput.Password = password;
                _session.Set<User>("Info", userInput);
                _context.SaveChanges();
                Console.WriteLine("Change information successfully!");
            }
            return Page();
        }
    }
}
