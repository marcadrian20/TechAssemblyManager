using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechAssemblyManager.Models
{
    public class OrderItem
    {
        public string ProductId { get; set; }
        public string PromotionId { get; set; } // Optional, for promotions
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsPromotion { get; set; } // True if this is a promotion cart item
    }
}