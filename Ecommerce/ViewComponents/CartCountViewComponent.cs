using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;

namespace Ecommerce.ViewComponents;

public class CartCountViewComponent : ViewComponent
{
    private readonly MyContext _context;

    public CartCountViewComponent(MyContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        string? custSession = HttpContext.Session.GetString("cust_session");
        if (string.IsNullOrEmpty(custSession))
        {
            return View(0);
        }

        int custId = int.Parse(custSession);

        var count = await _context.tbl_cart
            .Where(c => c.Cust_id == custId && c.Status == 0)
            .SumAsync(c => c.Prod_quantity);

        return View(count);
    }
}
