using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;

namespace Ecommerce.Controllers;

public class CustomerController : Controller
{
    private readonly MyContext _context;

    public CustomerController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Signup(TblCustomer customer, IFormFile? file)
    {
        if (file != null)
        {
            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product_image", filename);
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            customer.Cust_image = filename;
        }
        else
        {
            customer.Cust_image = "default_user.png";
        }

        await _context.tbl_customer.AddAsync(customer);
        await _context.SaveChangesAsync();
        
        TempData["Success"] = "Account created successfully! Please login.";
        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var cust = await _context.tbl_customer.FirstOrDefaultAsync(c => c.Cust_email == email && c.Cust_password == password);
        if (cust != null)
        {
            HttpContext.Session.SetString("cust_session", cust.Cust_id.ToString());
            HttpContext.Session.SetString("cust_name", cust.Cust_name);
            return RedirectToAction("Index", "Home");
        }
        
        ViewBag.Error = "Invalid Email or Password";
        return View();
    }

    public async Task<IActionResult> Profile()
    {
        string? custId = HttpContext.Session.GetString("cust_session");
        if (string.IsNullOrEmpty(custId))
        {
            return RedirectToAction("Login");
        }

        var customer = await _context.tbl_customer.FindAsync(int.Parse(custId));
        return View(customer);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
