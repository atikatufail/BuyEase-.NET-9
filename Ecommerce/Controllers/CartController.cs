using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;

namespace Ecommerce.Controllers;

public class CartController : Controller
{
    private readonly MyContext _context;

    public CartController(MyContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        string? custSession = HttpContext.Session.GetString("cust_session");
        if (string.IsNullOrEmpty(custSession))
        {
            return RedirectToAction("Login", "Customer");
        }

        int custId = int.Parse(custSession);

        var cartItems = await _context.tbl_cart
            .Include(c => c.Product)
            .Where(c => c.Cust_id == custId && c.Status == 0) // Status 0 = In Cart
            .ToListAsync();

        return View(cartItems);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int prodId)
    {
        string? custSession = HttpContext.Session.GetString("cust_session");
        if (string.IsNullOrEmpty(custSession))
        {
            return RedirectToAction("Login", "Customer");
        }

        int custId = int.Parse(custSession);

        var existingItem = await _context.tbl_cart
            .FirstOrDefaultAsync(c => c.Prod_id == prodId && c.Cust_id == custId && c.Status == 0);

        if (existingItem != null)
        {
            existingItem.Prod_quantity++;
            _context.tbl_cart.Update(existingItem);
        }
        else
        {
            var cartItem = new TblCart
            {
                Prod_id = prodId,
                Cust_id = custId,
                Prod_quantity = 1,
                Status = 0
            };
            await _context.tbl_cart.AddAsync(cartItem);
        }

        await _context.SaveChangesAsync();
        TempData["CartMessage"] = "Item added in cart";
        return Redirect(Request.Headers["Referer"].ToString() ?? "/");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int cartId)
    {
        var item = await _context.tbl_cart.FindAsync(cartId);
        if (item != null)
        {
            _context.tbl_cart.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> PlusProduct(int cartId)
    {
        string? custSession = HttpContext.Session.GetString("cust_session");
        if (string.IsNullOrEmpty(custSession)) return RedirectToAction("Login", "Customer");

        var item = await _context.tbl_cart.FindAsync(cartId);
        if (item != null)
        {
            item.Prod_quantity++;
            _context.tbl_cart.Update(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> MinusProduct(int cartId)
    {
        string? custSession = HttpContext.Session.GetString("cust_session");
        if (string.IsNullOrEmpty(custSession)) return RedirectToAction("Login", "Customer");

        var item = await _context.tbl_cart.FindAsync(cartId);
        if (item != null)
        {
            if (item.Prod_quantity > 1)
            {
                item.Prod_quantity--;
                _context.tbl_cart.Update(item);
            }
            else
            {
                _context.tbl_cart.Remove(item);
            }
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}
