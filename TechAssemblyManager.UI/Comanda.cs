using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public class Comanda
    {
        public List<Produs> Produse { get; set; }
        public string Nume { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public DateTime DataComenzii { get; set; }
        public string Status { get; internal set; }

        public Comanda(List<Produs> produse, string nume, string adresa, string telefon, string email)
        {
            Produse = produse;
            Nume = nume;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            DataComenzii = DateTime.Now;
        }
    }
}
