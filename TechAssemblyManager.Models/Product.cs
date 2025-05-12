using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechAssemblyManager.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        [Range(1, 5)]
        public int Rating { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual ProductCategory Category { get; set; }
        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PromotionItem> PromotionItems { get; set; }
    }
}