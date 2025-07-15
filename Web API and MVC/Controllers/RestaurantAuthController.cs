using HotBytee.MVC.Data;
using HotBytee.MVC.DTO;
using HotBytee.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace HotBytee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantAuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ POST: api/RestaurantAuth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RestaurantRegisterDto dto)
        {
            if (_context.Restaurants.Any(r => r.ContactEmail == dto.ContactEmail))
                return BadRequest("Email already registered.");

            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Address = dto.Address,
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                Description = dto.Description,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();

            return Ok(new { message = "Registration successful." });
        }

        // ✅ POST: api/RestaurantAuth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] RestaurantLoginDto dto)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.ContactEmail == dto.ContactEmail);
            if (restaurant == null)
                return Unauthorized("Email not found.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, restaurant.PasswordHash))
                return Unauthorized("Incorrect password.");

            // ❗ Replace this with real JWT generation later
            var fakeToken = $"FAKE-TOKEN-{restaurant.RestaurantId}";

            return Ok(new
            {
                message = "Login successful.",
                restaurantId = restaurant.RestaurantId,
                token = fakeToken
            });
        }
    }
}
