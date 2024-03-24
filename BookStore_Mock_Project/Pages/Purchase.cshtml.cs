using BookStore_Mock_Project.Data;
using BookStore_Mock_Project.Model;
using BookStore_Mock_Project.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace BookStore_Mock_Project.Pages
{

    public class PurchaseModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SessionService _session;

        public PurchaseModel(ApplicationDbContext context, SessionService session)
        {
            _context = context;
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public List<CartItem> Cart { get; set; } = default!;
        public void OnGet()
        {
            Cart = _session.Get<List<CartItem>>("cart");
            if (Cart != null)
            {
                ViewData["total"] = Cart.Sum(s => s.SubTotal);
            }
            else
            {
                Cart = new List<CartItem>();
                ViewData["total"] = 0;
            }
        }
        public async Task<IActionResult> OnPostAddToCartAsync(Guid? id, int quantity = 1)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            Cart = _session.Get<List<CartItem>>("cart");
            if (Cart == null)
            {
                Cart = new List<CartItem>();
                Cart.Add(new CartItem { Book = book, Quantity = quantity });
            }
            else
            {
                int index = Cart.FindIndex(c => c.Book.BookId == id);
                if (index != -1) //if item already in the cart
                {
                    Cart[index].Quantity++; //increment by 1
                }
                else
                {
                    Cart.Add(new CartItem { Book = book, Quantity = quantity });
                }
            }
            _session.Set<List<CartItem>>("cart", Cart);
            return RedirectToPage("/Purchase");
        }
        public async Task<IActionResult> OnPostAddQtyAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            Cart = _session.Get<List<CartItem>>("cart");

            int index = Cart.FindIndex(c => c.Book.BookId == id);
            Cart[index].Quantity++; //increment by 1

            _session.Set<List<CartItem>>("cart", Cart);
            return RedirectToPage("/Purchase");
        }
        public async Task<IActionResult> OnPostMinusQtyAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            Cart = _session.Get<List<CartItem>>("cart");

            int index = Cart.FindIndex(c => c.Book.BookId == id);
            if (Cart[index].Quantity == 1)
            {
                Cart.RemoveAt(index);
            }
            else
            {
                Cart[index].Quantity--; //reduce by 1
            }

            _session.Set<List<CartItem>>("cart", Cart);
            return RedirectToPage("/Purchase");
        }
    }
}
