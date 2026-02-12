using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using System.Diagnostics;

namespace Ecommerce.Controllers;

public class HomeController : Controller
{
    private readonly MyContext _context;

    public HomeController(MyContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Display top 8 products as featured
        var featuredProducts = await _context.tbl_product
            .Include(p => p.Category)
            .OrderByDescending(p => p.Prod_id)
            .Take(8)
            .ToListAsync();
        return View(featuredProducts);
    }

    public async Task<IActionResult> Shop(int? categoryId, string? search)
    {
        var productsQuery = _context.tbl_product.Include(p => p.Category).AsQueryable();

        if (categoryId.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.Cat_id == categoryId.Value);
        }

        if (!string.IsNullOrEmpty(search))
        {
            productsQuery = productsQuery.Where(p => p.Prod_name.Contains(search) || p.Prod_description.Contains(search));
        }

        var products = await productsQuery.ToListAsync();
        ViewBag.Categories = await _context.tbl_category.ToListAsync();
        ViewBag.SelectedCategory = categoryId;
        ViewBag.SearchTerm = search;
        
        return View(products);
    }

    public async Task<IActionResult> ProductDetails(int id)
    {
        var product = await _context.tbl_product
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Prod_id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public async Task<IActionResult> Category()
    {
        var categories = await _context.tbl_category.ToListAsync();
        return View(categories);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
