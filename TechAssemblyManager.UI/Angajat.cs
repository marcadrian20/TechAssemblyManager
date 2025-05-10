using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public abstract class Angajat : MainForm.User
    {
        public string Nume { get; set; }
        public string Email { get; set; }

        protected Angajat(string nume, string email) : base(nume, email)
        {
            Nume = nume;
            Email = email;
        }

        public abstract void OnoreazaComanda(Comanda comanda);
        public abstract void ActualizeazaStatusService(CerereService cerere,string statusNou);
        public override string ToString()
        {
            return $"{Nume} - {Email}";
        }
    }
}
