﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore_Mock_Project.Data;
using BookStore_Mock_Project.Model;

namespace BookStore_Mock_Project.Pages.Admin.Category_Page
{
    public class PermDeleteModel : PageModel
    {
        private readonly BookStore_Mock_Project.Data.ApplicationDbContext _context;

        public PermDeleteModel(BookStore_Mock_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookCategory BookCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            var bookcategory = await _context.BookCategories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (bookcategory == null)
            {
                return NotFound();
            }
            else 
            {
                BookCategory = bookcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.BookCategories == null)
            {
                return NotFound();
            }
            var bookcategory = await _context.BookCategories.FindAsync(id);

            if (bookcategory != null)
            {
                BookCategory = bookcategory;
                _context.BookCategories.Remove(BookCategory);
                await _context.SaveChangesAsync();
            }
            TempData["Notify"] = BookCategory.Name + " is permanently removed!";
            return RedirectToPage("./Index");
        }
    }
}
