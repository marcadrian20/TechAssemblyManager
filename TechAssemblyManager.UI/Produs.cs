using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public class Produs
    {
        public string Nume { get; set; }
        public decimal Pret { get; set; }
        public Image Imagine { get; set; }
        public string Categorie { get; set; }
        public override string ToString()
        {
            return $"{Nume} - {Pret} RON";
        }


    }
}
