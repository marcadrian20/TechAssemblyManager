using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public class AngajatSenior : Angajat
    {
        public string TipCont { get; set; }
        public AngajatSenior(string nume, string email) : base(nume, email)
        {
            TipCont = "Senior";
        }

        public void AdaugaProdusNou(Produs produs)
        {
            // implementare logica de adăugare produs
        }

        public override void OnoreazaComanda(Comanda comanda)
        {
            // implementare logica specifică
        }

        public override void ActualizeazaStatusService(CerereService cerere, string statusNou)
        {
            cerere.Status = statusNou;
        }
    }
}
