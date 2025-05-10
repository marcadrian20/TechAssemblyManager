using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public class Promotie
    {
        public string Nume { get; set; }
        public string Descriere { get; set; }
        public DateTime DataInceput { get; set; }
        public DateTime DataSfarsit { get; set; }

        public List<Produs> ProduseIncluse { get; set; } = new List<Produs>();
        public decimal Discount => ProduseIncluse.Sum(p => p.Pret) * 0.1m;

        public Produs GetArticolDiscount()
        {
            return new Produs
            {
                Nume = $"Reducere promoție: {Nume}",
                Pret = -Discount,
                Descriere = "Reducere 10% la promoție",
                ScorCritici = 5,
                Categorie = "Promoție"
            };
        }
        public override string ToString()
        {
            return $" {Nume} ({ProduseIncluse.Count} produse);  {Nume} ({DataInceput.ToShortDateString()} - {DataSfarsit.ToShortDateString()})";
        }
    }

}
