using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }= DateTime.Now;
        
        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public OrderType OrderType { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }= OrderStatus.PENDING;

        [StringLength(1000)]
        public string OrderDescription { get; set; }

        public DateTime? CompletionDate { get; set; }

        public virtual Client Client { get; set; }

        //FKeys
        public int ClientId { get; set; }
        public int AssignedEmployeeId { get; set; }

        [ForeignKey("ClientId")]
        public virtual User AssignedClient { get; set; }

        [ForeignKey("AssignedEmployeeId")]
        public virtual User AssignedEmployee { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
    public enum OrderType
    {
        SERVICE,
        PURCHASE,
        ASSEMBLY
    }
    public enum OrderStatus
    {
        PENDING,
        INPROGRESS,
        COMPLETED,
        CANCELLED
    }
}
