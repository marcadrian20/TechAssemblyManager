using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public class AngajatJunior : Angajat
    {
        public string TipCont { get; set; }
        public AngajatJunior(string nume, string email) : base(nume, email)
        {
            TipCont = "Junior";
        }

        public override void OnoreazaComanda(Comanda comanda)
        {
            comanda.Status = "Onorată de junior";
        }

        public void SchimbaStatusService(CerereService cerere, string noulStatus)
        {
            cerere.Status = noulStatus;
        }

        public override void ActualizeazaStatusService(CerereService cerere,string statusNou)
        {
            cerere.Status = statusNou;
        }
    }
}
