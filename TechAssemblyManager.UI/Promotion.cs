using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public class Promotion
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }

        public Promotion(string name, string description, decimal discount)
        {
            Name = name;
            Description = description;
            Discount = discount;
        }
    }


}
