namespace HotBytee.MVC.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }  // Breakfast, Lunch, etc.
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public string TasteInfo { get; set; }  // Spicy, Sweet, etc.
        public string NutritionalInfo { get; set; }  // Optional
        public string DietaryInfo { get; set; } // Veg / Non-Veg

        public int RestaurantId { get; set; }
    }
}