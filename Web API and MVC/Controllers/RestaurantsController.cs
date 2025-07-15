using HotBytee.MVC.Data;
using HotBytee.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotBytee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Restaurants
        [HttpGet]
        public IActionResult GetAll()
        {
            var restaurants = _context.Restaurants.ToList();
            return Ok(restaurants);
        }

        // ✅ GET: api/Restaurants/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        // ✅ POST: api/Restaurants
        [HttpPost]
        public IActionResult Create(Restaurant model)
        {
            _context.Restaurants.Add(model);
            _context.SaveChanges();
            return Ok(model);
        }

        // ✅ PUT: api/Restaurants/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Restaurant model)
        {
            var existing = _context.Restaurants.Find(id);
            if (existing == null)
                return NotFound();

            existing.Name = model.Name;
            existing.Address = model.Address;
            existing.ContactEmail = model.ContactEmail;
            existing.ContactPhone = model.ContactPhone;
            existing.Description = model.Description;

            _context.SaveChanges();
            return Ok(existing);
        }

        // ✅ DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant == null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
