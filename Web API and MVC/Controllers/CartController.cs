using HotBytee.MVC.Data;
using HotBytee.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Display cart items
    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    // Add item to cart
    [HttpPost]
    public IActionResult AddToCart(int menuItemId)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            TempData["Message"] = "You need to log in to add items to the cart.";
            return RedirectToAction("Login", "Account");
        }

        var item = _context.MenuItems.FirstOrDefault(m => m.MenuItemId == menuItemId);
        if (item == null) return NotFound();

        var cart = GetCart();
        var existingItem = cart.FirstOrDefault(c => c.MenuItemId == menuItemId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem
            {
                MenuItemId = item.MenuItemId,
                Name = item.Name,
                ImageUrl = item.ImageUrl,
                Price = item.Price,
                Quantity = 1
            });
        }

        SaveCart(cart);
        UpdateCartCount(cart);

        return RedirectToAction("Index", "Menu");
    }

    // Remove item from cart
    [HttpPost]
    public IActionResult RemoveFromCart(int menuItemId)
    {
        var cart = GetCart();
        var itemToRemove = cart.FirstOrDefault(c => c.MenuItemId == menuItemId);
        if (itemToRemove != null)
        {
            cart.Remove(itemToRemove);
            SaveCart(cart);
            UpdateCartCount(cart);
        }

        return RedirectToAction("Index");
    }

    // Increase item quantity
    [HttpPost]
    public IActionResult IncreaseQuantity(int menuItemId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.MenuItemId == menuItemId);
        if (item != null)
        {
            item.Quantity++;
            SaveCart(cart);
            UpdateCartCount(cart);
        }
        return RedirectToAction("Index");
    }

    // Decrease item quantity
    [HttpPost]
    public IActionResult DecreaseQuantity(int menuItemId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.MenuItemId == menuItemId);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0)
            {
                cart.Remove(item);
            }
            SaveCart(cart);
            UpdateCartCount(cart);
        }
        return RedirectToAction("Index");
    }

    // Place order with shipping address
    [HttpPost]
    public IActionResult PlaceOrder(string shippingAddress)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            TempData["Message"] = "Please log in to place an order.";
            return RedirectToAction("Login", "Account");
        }

        var cart = GetCart();
        if (!cart.Any())
            return RedirectToAction("Index");

        var order = new Order
        {
            UserId = userId.Value,
            OrderDate = DateTime.Now,
            Status = "Pending",
            TotalAmount = cart.Sum(i => i.Price * i.Quantity),
            ShippingAddress = shippingAddress
        };

        _context.Orders.Add(order);
        _context.SaveChanges();

        foreach (var item in cart)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.OrderId,
                MenuItemId = item.MenuItemId,
                ItemName = item.Name,
                UnitPrice = item.Price,
                Quantity = item.Quantity
            };
            _context.OrderItems.Add(orderItem);
        }

        _context.SaveChanges();

        HttpContext.Session.Remove("Cart");
        HttpContext.Session.Remove("CartCount");

        TempData["Message"] = "Order placed successfully!";
        return RedirectToAction("Index", "Menu");
    }

    // View past orders
    public IActionResult OrderHistory()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var orders = _context.Orders
            .Where(o => o.UserId == userId.Value)
            .Include(o => o.Items)
            .ToList();

        return View(orders);
    }

    // Helper: Get cart from session
    private List<CartItem> GetCart()
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        return string.IsNullOrEmpty(cartJson)
            ? new List<CartItem>()
            : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
    }

    // Helper: Save cart to session
    private void SaveCart(List<CartItem> cart)
    {
        var cartJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString("Cart", cartJson);
    }

    // Helper: Update cart count in session
    private void UpdateCartCount(List<CartItem> cart)
    {
        int count = cart.Sum(i => i.Quantity);
        HttpContext.Session.SetString("CartCount", count.ToString());
    }
}
