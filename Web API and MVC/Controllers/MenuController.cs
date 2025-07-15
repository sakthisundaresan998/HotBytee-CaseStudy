using HotBytee.MVC.Data;
using HotBytee.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

public class MenuController : Controller
{
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Menu/Index
    public IActionResult Index()
    {
        // ✅ Seed sample items only once (if DB is empty)
        if (!_context.MenuItems.Any())
        {
            var sampleMenus = new List<MenuItem>
            {
                new MenuItem
                {
                    Name = "Chicken Burger",
                    Category = "Burger",
                    Price = 150,
                    DietaryInfo = "Non-Veg",
                    TasteInfo = "Spicy",
                    NutritionalInfo = "500 kcal",
                    Description = "Juicy grilled chicken patty with crispy lettuce and creamy mayo.",
                    ImageUrl = "/images/burger.jpg",
                    IsAvailable = true
                },
                new MenuItem
                {
                    Name = "Veg Margherita Pizza",
                    Category = "Pizza",
                    Price = 180,
                    DietaryInfo = "Veg",
                    TasteInfo = "Mild",
                    NutritionalInfo = "600 kcal",
                    Description = "Classic Italian pizza with mozzarella cheese and tomato base.",
                    ImageUrl = "/images/veg.webp",
                    IsAvailable = true
                },
                new MenuItem
                {
                    Name = "Chocolate Brownie",
                    Category = "Dessert",
                    Price = 120,
                    DietaryInfo = "Veg",
                    TasteInfo = "Sweet",
                    NutritionalInfo = "450 kcal",
                    Description = "Rich chocolate brownie served warm with melted chocolate.",
                    ImageUrl = "/images/brownie.webp",
                    IsAvailable = true
                }
            };

            _context.MenuItems.AddRange(sampleMenus);
            _context.SaveChanges();
        }



        // ✅ Fetch available items
        var items = _context.MenuItems.Where(m => m.IsAvailable).ToList();

        return View(items);
    }

    public IActionResult Details(int id)
    {
        var item = _context.MenuItems.FirstOrDefault(m => m.MenuItemId == id);
        if (item == null)
            return NotFound();

        return View(item);
    }
}