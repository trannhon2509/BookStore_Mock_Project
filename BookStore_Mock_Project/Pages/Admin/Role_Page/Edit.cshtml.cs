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

namespace BookStore_Mock_Project.Pages.Admin.Role_Page
{
    public class EditModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public EditModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Role Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role =  await _context.Roles.FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }
            Role = role;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(Role.RoleId))
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

        private bool RoleExists(Guid id)
        {
          return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
