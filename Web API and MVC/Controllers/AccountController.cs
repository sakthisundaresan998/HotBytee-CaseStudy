using HotBytee.MVC.Data;
using HotBytee.MVC.Models;
using HotBytee.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Account/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if email already exists
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password, // Hash in real projects
                Address = model.Address,
                ContactNumber = model.ContactNumber
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        return View(model);
    }

    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                // ✅ Set session
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.Name);

                // ✅ Clear leftover login prompt message if any
                TempData.Remove("Message");

                return RedirectToAction("Index", "Menu");
            }

            ModelState.AddModelError("", "Invalid email or password.");
        }

        return View(model);
    }

    // GET: /Account/Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }
}
