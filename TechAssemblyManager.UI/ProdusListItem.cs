using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    class ProdusListItem
    {
        public Produs Produs { get; }

        public ProdusListItem(Produs produs)
        {
            Produs = produs;
        }

        public override string ToString()
        {
            return $"{Produs.Nume} - {Produs.Pret} RON";
        }
    }
}
