using HotBytee.MVC.Data;
using HotBytee.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotBytee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MenuItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/MenuItems/by-restaurant/5
        [HttpGet("by-restaurant/{restaurantId}")]
        public ActionResult<IEnumerable<MenuItem>> GetByRestaurant(int restaurantId)
        {
            return _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .ToList();
        }

        // ✅ GET: api/MenuItems/5
        [HttpGet("{id}")]
        public ActionResult<MenuItem> Get(int id)
        {
            var item = _context.MenuItems.Find(id);
            if (item == null) return NotFound();
            return item;
        }

        // ✅ POST: api/MenuItems
        [HttpPost]
        public IActionResult Create([FromBody] MenuItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.MenuItems.Add(item);
            _context.SaveChanges();
            return Ok(new { message = "Menu item added", id = item.MenuItemId });
        }

        // ✅ PUT: api/MenuItems/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MenuItem updatedItem)
        {
            var existing = _context.MenuItems.Find(id);
            if (existing == null) return NotFound();

            existing.Name = updatedItem.Name;
            existing.Category = updatedItem.Category;
            existing.Price = updatedItem.Price;
            existing.Description = updatedItem.Description;
            existing.DietaryInfo = updatedItem.DietaryInfo;
            existing.TasteInfo = updatedItem.TasteInfo;
            existing.NutritionalInfo = updatedItem.NutritionalInfo;
            existing.ImageUrl = updatedItem.ImageUrl;
            existing.IsAvailable = updatedItem.IsAvailable;
            existing.RestaurantId = updatedItem.RestaurantId;

            _context.SaveChanges();
            return Ok(new { message = "Updated successfully" });
        }

        // ✅ DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.MenuItems.Find(id);
            if (item == null) return NotFound();

            _context.MenuItems.Remove(item);
            _context.SaveChanges();
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
