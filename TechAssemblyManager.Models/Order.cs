using System;
using System.Collections.Generic;

namespace TechAssemblyManager.Models
{
    public class Order
    {
        public string OrderId { get; set; } // Firebase key
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        public decimal TotalCost { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDescription { get; set; }
        public DateTime? CompletionDate { get; set; }

        public string ClientUserName { get; set; }
        public string AssignedEmployeeUserName { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}