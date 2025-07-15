using System.ComponentModel.DataAnnotations;

namespace HotBytee.MVC.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        [Phone]
        public string ContactPhone { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string PasswordHash { get; set; } 
    }
}
