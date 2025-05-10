using System.ComponentModel.DataAnnotations;

namespace TechAssemblyManager.Models
{
    public class Employee : User
    {
        [Required]
        public bool IsSenior { get; set; } = false;

        [Required]
        public bool IsManager { get; set; } = false;

        public virtual ICollection<Order> HandledOrders { get; set; }
        public virtual ICollection<ServiceRequest> HandledServiceRequests { get; set; }

        public virtual ICollection<Promotion> CreatedPromotions { get; set; }
        public virtual ICollection<Product> CreatedProducts { get; set; }
    }
}
