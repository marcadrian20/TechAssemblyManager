namespace TechAssemblyManager.Models
{
    public class PromotionCartItem
    {
        public string PromotionId { get; set; }
        public decimal DiscountAmount { get; set; } // Negative value
        public string Description { get; set; }
    }
}