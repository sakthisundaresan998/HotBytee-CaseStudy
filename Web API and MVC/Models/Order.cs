using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotBytee.MVC.Models
{
    public class Order
    {
        public int OrderId { get; set; }           // Primary Key
        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }         // Placed, Preparing, Delivered
        public string ShippingAddress { get; set; }

        // ✅ Navigation to order items
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }

        // ✅ Foreign key to Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // ✅ Foreign key to MenuItem (optional, for tracking)
        public int MenuItemId { get; set; }

        public string ItemName { get; set; }       // Store name in case menu changes
        public decimal UnitPrice { get; set; }     // ✅ Store price at time of order
        public int Quantity { get; set; }
        public MenuItem MenuItem { get; set; }

        [NotMapped]
        public decimal Subtotal => UnitPrice * Quantity;
    }
}