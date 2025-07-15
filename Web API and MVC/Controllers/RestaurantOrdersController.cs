using Final_CaseStudy.DTO;
using HotBytee.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotBytee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("by-restaurant/{restaurantId}")]
        public IActionResult GetOrdersByRestaurant(int restaurantId)
        {
            var orders = _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.MenuItem)
                .Where(o => o.Items.Any(i => i.MenuItem.RestaurantId == restaurantId))
                .ToList();

            Console.WriteLine($"Found {orders.Count} orders for RestaurantId: {restaurantId}");

            return Ok(orders);
        }

        // PUT: api/RestaurantOrders/update-status/{orderId}
        [HttpPut("{orderId}/status")]
        public IActionResult UpdateStatus(int orderId, [FromBody] UpdateStatusDto dto)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return NotFound();

            order.Status = dto.NewStatus;
            _context.SaveChanges();

            return Ok(new { message = "Status updated." });
        }




    }
}
