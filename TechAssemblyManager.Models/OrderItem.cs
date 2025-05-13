using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechAssemblyManager.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        //FKeys
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int PromotionId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("PromotionId")]
        //public virtual Promotion Promotion { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}