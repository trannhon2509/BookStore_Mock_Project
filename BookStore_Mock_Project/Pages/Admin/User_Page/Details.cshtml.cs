using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore_Mock_Project.Data;
using BookStore_Mock_Project.Model;

namespace BookStore_Mock_Project.Pages.Admin.User_Page
{
    public class DetailsModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public DetailsModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
