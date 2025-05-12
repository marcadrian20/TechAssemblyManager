using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechAssemblyManager.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public CategoryType Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

public enum CategoryType
{
    DesktopPC,
    LaptopPC,
    Printer,
    Peripheral,
    Component
}