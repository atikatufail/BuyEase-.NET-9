using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminController : Controller
{
    private MyContext _context;
    private IWebHostEnvironment _env;
    public AdminController(MyContext context, IWebHostEnvironment env) //ye 'context' sirf issi curly braces tak available hay isko overall accessible bananay k liye global variable kay ref main daina paryga (jo upar likha hy)
    {
        _context = context; 
        _env = env;
    }
    public IActionResult Index()
    {
        string admin_session = HttpContext.Session.GetString("admin_session");
        if(admin_session!=null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Login");
        }
    }

    public IActionResult Login()   // GET
    {
        return View();
    }


    //httppost ka kam: form ka data collect kar k compare karwana aur dekhna kay forms say anay wali values db say match horhi hain ya nahi
    [HttpPost]
    public IActionResult Login(string adminEmail, string adminPassword)
    {
        var row = _context.tbl_admin.FirstOrDefault(a=>a.Admin_email==adminEmail);  //select * from tbl_admin where admin_email = "atika@gmail.com"

        if(row != null && row.Admin_password == adminPassword)
        {
            //create session
        
            HttpContext.Session.SetString("admin_session", row.Admin_id.ToString());  //session key, value main data layta hy
            return RedirectToAction("index");
        }
        else
        {
            ViewBag.message = "Incorrect Username or Password";                 
            return View();
        }
        
        // ye sirf display karwa raha login ka method 
        
    }

    public IActionResult Logout()   // GET
    {
        HttpContext.Session.Remove("admin_session");
        return RedirectToAction("login") ;
    }

    public IActionResult Profile()
    {
        var adminId = HttpContext.Session.GetString("admin_session");
        var row = _context.tbl_admin.Where(a=>a.Admin_id == int.Parse(adminId)).ToList(); // .Find sirf id say data layta .Where kisi bhe column say fetch karwa skta
        return View(row);
    }

    [HttpPost]
    public IActionResult Profile(TblAdmin admin)
    {
        _context.tbl_admin.Update(admin);
        _context.SaveChanges();
        return RedirectToAction("Profile");
    }

    [HttpPost]
    public IActionResult ChangeProfileImage(IFormFile Admin_image, TblAdmin admin)
    {
        // 1. Safety Check: If no file was selected, just go back
        if (Admin_image == null || Admin_image.Length == 0)
        {
            return RedirectToAction("Profile");
        }

        // 2. Save the physical file to the folder
        string ImagePath = Path.Combine(_env.WebRootPath, "admin_image", Admin_image.FileName);
        using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
        {
            Admin_image.CopyTo(fs);
        }

        // 3. Update the Database
        // We update only the image column for the admin with this specific ID
        var existingAdmin = _context.tbl_admin.Find(admin.Admin_id);
        if (existingAdmin != null)
        {
            existingAdmin.Admin_image = Admin_image.FileName;
            _context.SaveChanges();
        }

        return RedirectToAction("Profile");
    }

    public IActionResult FetchCustomer()
    {
        return View(_context.tbl_customer.ToList());
    }

    public IActionResult customerDetails(int id)
    {
        return View(_context.tbl_customer.FirstOrDefault(c=> c.Cust_id == id));
    }

    public IActionResult updateCustomer(int id)
    {
        return View(_context.tbl_customer.Find(id));
    }

    [HttpPost]
    public IActionResult updateCustomer(TblCustomer customer, IFormFile Cust_image)
    {
        var existingCust = _context.tbl_customer.Find(customer.Cust_id);
        if (existingCust != null)
        {
            // Update all fields
            existingCust.Cust_name = customer.Cust_name;
            existingCust.Cust_email = customer.Cust_email;
            existingCust.Cust_password = customer.Cust_password;
            existingCust.Cust_phone = customer.Cust_phone;
            existingCust.Cust_address = customer.Cust_address;
            existingCust.Cust_country = customer.Cust_country;
            existingCust.Cust_city = customer.Cust_city;
            existingCust.Cust_gender = customer.Cust_gender;

            // Handle image if provided
            if (Cust_image != null && Cust_image.Length > 0)
            {
                string ImagePath = Path.Combine(_env.WebRootPath, "customer_image", Cust_image.FileName);
                using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
                {
                    Cust_image.CopyTo(fs);
                }
                existingCust.Cust_image = Cust_image.FileName;
            }
            
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Customer updated successfully";
            return RedirectToAction("FetchCustomer");
        }

        return View(customer);
    }

    public IActionResult deleteCustomer(int id)
    {
        var customer = _context.tbl_customer.Find(id);
        _context.tbl_customer.Remove(customer);
        _context.SaveChanges();
        return RedirectToAction("FetchCustomer");
    }

    public IActionResult deletePermission(int id)
    {
        return View(_context.tbl_customer.FirstOrDefault(c=> c.Cust_id == id));
    }

    public IActionResult fetchCategory()
    {
        return View(_context.tbl_category.ToList());
    }

    //just to display form
    public IActionResult addCategory()
    {
        return View();
    }

    //form say data collect kar kay category walay table main insert
    [HttpPost]
    public IActionResult addCategory(TblCategory cat)
    {
        _context.tbl_category.Add(cat);
        _context.SaveChanges();
        return RedirectToAction("fetchCategory");
    }
    public IActionResult updateCategory(int id)
    {
        var category = _context.tbl_category.Find(id);
        return View(category);
    }

    [HttpPost]
   public IActionResult updateCategory(TblCategory cat)
    {
        _context.tbl_category.Update(cat);
        _context.SaveChanges();
        return RedirectToAction("fetchCategory");
    }
    
    public IActionResult deletePermissionCategory(int id)
    {
        return View(_context.tbl_category.FirstOrDefault(c=> c.Category_id == id));
    }

    public IActionResult deleteCategory(int id)
    {
        var category = _context.tbl_category.Find(id);
        _context.tbl_category.Remove(category);
        _context.SaveChanges();
        return RedirectToAction("fetchCategory");
    }

    public IActionResult fetchProduct()
    {
        return View(_context.tbl_product.Include(p => p.Category).ToList());
    }

    public IActionResult addProduct()
    {
        List<TblCategory> categories = _context.tbl_category.ToList();
        ViewData["category"] = categories;
        return View();
    }

    
        [HttpPost]
    public IActionResult addProduct(TblProduct prod, IFormFile Prod_image)
    {
        // 1. Handle the Image Upload
        if (Prod_image != null && Prod_image.Length > 0)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "product_image", Prod_image.FileName);
            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                Prod_image.CopyTo(fs);
            }
            // Assign the filename to the model
            prod.Prod_image = Prod_image.FileName;
        }

        // 2. Add the NEW product to the database
        // Don't use .Find() here because the product is new!
        _context.tbl_product.Add(prod); 
        _context.SaveChanges();

        // 3. Redirect to the list of products
        return RedirectToAction("fetchProduct");
    }

    public IActionResult productDetails(int id)
    {
        return View(_context.tbl_product.Include(p=>p.Category).FirstOrDefault(p=>p.Prod_id==id));
    }

    public IActionResult deletePermissionPrroduct(int id)
    {
        return View(_context.tbl_product.FirstOrDefault(p=> p.Prod_id == id));
    }

    public IActionResult deleteProduct(int id)
    {
        var product = _context.tbl_product.Find(id);
        _context.tbl_product.Remove(product);
        _context.SaveChanges();
        return RedirectToAction("fetchProduct");
    }

    public IActionResult updateProduct(int id)
    {
        List<TblCategory> categories = _context.tbl_category.ToList();
        ViewData["category"] = categories;
        var product = _context.tbl_product.Find(id);
        ViewBag.selectedCategoryId = product.Cat_id; 
        return View(product);
    }

    [HttpPost]
   public IActionResult updateProduct(TblProduct prod)
    {
        _context.tbl_product.Update(prod);
        _context.SaveChanges();
        return RedirectToAction("fetchProduct");
    }

    public IActionResult ChangeProductImage(IFormFile product_image, TblProduct product)
    {
        // 1. Safety Check: If no file was selected, just go back
        if (product_image == null || product_image.Length == 0)
        {
            return RedirectToAction("Profile");
        }

        // 2. Save the physical file to the folder
        string ImagePath = Path.Combine(_env.WebRootPath, "product_image", product_image.FileName);
        using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
        {
            product_image.CopyTo(fs);
        }

        // 3. Update the Database
        // We update only the image column for the admin with this specific ID
        var existingProduct = _context.tbl_product.Find(product.Prod_id);
        if (existingProduct != null)
        {
            existingProduct.Prod_image = product_image.FileName;
            _context.SaveChanges();
        }

        return RedirectToAction("fetchProduct");
    }
    public IActionResult FetchCarts()
    {
        var carts = _context.tbl_cart
            .Include(c => c.Product)
            .Include(c => c.Product.Category)
            .Include(c => c.Customer)
            .Where(c => c.Status == 0) // Only active items in cart
            .ToList();
        return View(carts);
    }

}




