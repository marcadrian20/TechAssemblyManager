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
        public Comanda() { }
        public Comanda(List<Produs> produse, string nume, string adresa, string telefon, string email)
        {
            Produse = produse;
            Nume = nume;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            DataComenzii = DateTime.Now;
        }
        public List<Produs> getProduse()
        {
            return Produse;
        }
        public void setProduse(List<Produs> produse)
        {
            this.Produse = produse;
        }
        public string getNume()
        {
            return Nume;
        }
        public void setNume(string nume)
        {
            Nume = nume;
        }
        public string getAdresa()
        {
            return Adresa;
        }
        public void setAdresa(string adresa)
        {
            Adresa = adresa;
        }
        public string getTelefon()
        {
            return Telefon;
        }
        public void setTelefon(string telefon)
        {
            Telefon = telefon;
        }
        public string getEmail()
        {
            return Email;
        }
        public void setEmail(string email)
        {
            Email = email;
        }
        public DateTime getDataComenzii()
        {
            return DataComenzii;
        }
        public void setDataComenzii(DateTime dataComenzii)
        {
            DataComenzii = dataComenzii;
        }
        public string getStatus()
        {
            return Status;
        }
        public void setStatus(string status)
        {
            Status = status;
        }
        public override string ToString()
        {
            if (Produse == null || Produse.Count == 0)
                return "Nu sunt produse în comandă.";
            var toateProdusele = string.Join(", ", Produse.Select(p => p.ToString()));
            return $"Comanda pentru {Nume} - {Adresa} - {Telefon} - {Email} - {DataComenzii:dd/MM/yyyy} - {Status} - Produse: [{toateProdusele}]";

        }



    }
}
