export interface MenuItem {
  menuItemId?: number;         // Optional: for new items before save
  name: string;                // Name of the menu item
  category: string;            // Category like "Burger", "Pizza"
  price: number;               // Price in ₹
  dietaryInfo: string;         // Veg / Non-Veg
  description: string;         // Full description
  tasteInfo?: string;          // Optional: Spicy, Sweet, etc.
  nutritionalInfo?: string;    // Optional: "500 kcal"
  imageUrl?: string;           // Optional: link to image
  isAvailable: boolean;        // Is the item currently available?
  restaurantId: number;        // Foreign key to restaurant
}
