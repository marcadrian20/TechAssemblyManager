using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.Models
{
    public class Client : User
    {
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }

    }
}
