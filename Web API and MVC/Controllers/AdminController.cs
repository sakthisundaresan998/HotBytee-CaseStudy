using Final_CaseStudy.DTO;
using HotBytee.MVC.Data;
using HotBytee.MVC.DTO;
using HotBytee.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotBytee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Admin/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginDto loginDto)
        {
            var admin = _context.Admins
                .FirstOrDefault(a => a.Email == loginDto.Email && a.Password == loginDto.Password);

            if (admin == null)
                return Unauthorized("Invalid email or password");

            return Ok(new
            {
                message = "Login successful",
                adminId = admin.AdminId,
                token = "ADMIN-TOKEN-123"
            });
        }

        // GET: api/Admin/restaurants
        [HttpGet("restaurants")]
        public IActionResult GetRestaurants()
        {
            var restaurants = _context.Restaurants.ToList();
            return Ok(restaurants);
        }

        // POST: api/Admin/restaurants
        [HttpPost("restaurants")]
        public IActionResult AddRestaurant([FromBody] Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return Ok(restaurant);
        }

        // DELETE: api/Admin/restaurants/5
        [HttpDelete("restaurants/{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant == null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
            return NoContent();
        }

        // GET: api/Admin/menus
        [HttpGet("menus")]
        public IActionResult GetMenus()
        {
            var menus = _context.MenuItems.ToList();
            return Ok(menus);
        }

        // DELETE: api/Admin/menus/{id}
        [HttpDelete("menus/{id}")]
        public IActionResult DeleteMenuItem(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem == null)
                return NotFound();

            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();
            return NoContent();
        }


        // GET: api/Admin/orders
        [HttpGet("orders")]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders
                .Include(o => o.Items)
                .ToList();

            return Ok(orders);
        }

        [HttpPut("orders/{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order updated)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
                return NotFound();

            // ✅ Only update status (avoid replacing whole object)
            order.Status = updated.Status;
            _context.SaveChanges();

            return Ok(order);
        }


        // DELETE: api/Admin/orders/{id}
        [HttpDelete("orders/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            _context.OrderItems.RemoveRange(order.Items);
            _context.Orders.Remove(order);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
