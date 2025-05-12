using System.Collections.Generic;
using TechAssemblyManager.UI;

namespace TechAssemblyManager
{
    public static class AppState
    {
        private static List<Produs> produseInCos = new List<Produs>();
        private static List<Promotie> _promotii = new List<Promotie>();
        private static List<Angajat> angajati = new List<Angajat>()
        {
            new AngajatConcret("Ion Popescu", "ion.popescu@example.com"),
            new AngajatConcret("Maria Ionescu", "maria.ionescu@example.com")
        };

        public static void AdaugaPromotie(Promotie promotie) => _promotii.Add(promotie);
        public static void StergePromotie(Promotie promotie) => _promotii.Remove(promotie);
        public static List<Promotie> GetPromotii() => _promotii;
        private static List<CerereService> _cereri = new List<CerereService>();
        public static void AdaugaCerere(CerereService cerere) => _cereri.Add(cerere);
        public static List<CerereService> GetCereri() => _cereri;
        public static void ClearCereri() => _cereri.Clear();
        // ➕ Nou: Utilizator logat
        public static MainForm.User UtilizatorCurent { get; set; }

        // ➕ Nou: Shortcut pentru verificare logare
        public static bool EsteLogat => UtilizatorCurent != null;

        public static void AdaugaProdus(Produs produs)
        {
            produseInCos.Add(produs);

        }
        public static void AdaugaAngajat(Angajat angajat)
        {
            angajati.Add(angajat);

        }
        public static void StergeAngajat(Angajat angajat)
        {
            angajati.Remove(angajat);

        }
        public static List<Angajat> GetAngajati()
        {
            Console.WriteLine($"Number of employees in AppState: {angajati.Count}");
            foreach (var angajat in angajati)
            {
                Console.WriteLine($"Employee: {angajat.Nume} ({angajat.Email})");
            }
            return new List<Angajat>(angajati);
        }
        public static List<Produs> GetProduse()
        {
            return new List<Produs>(produseInCos);
        }

        public static void SetProduse(List<Produs> produse)
        {
            produseInCos = produse;
        }

        public static void Clear()
        {
            produseInCos.Clear();
            UtilizatorCurent = null; // Resetare stare utilizator la logout
        }
        public static void AdaugaCerereService(CerereService cerere)
        {
            _cereri.Add(cerere);
        }

        public static List<CerereService> GetCereriService()
        {
            return new List<CerereService>(_cereri);
        }

    }

    // Fix for CS7036: Ensure the base constructor of Angajat is called with the required parameters.
    public class AngajatConcret : Angajat
    {
        public AngajatConcret(string nume, string email) : base(nume, email) // Pass parameters to the base class constructor
        {
            // No additional initialization needed here as the base constructor handles it
        }

        public override void OnoreazaComanda(Comanda comanda)
        {
            if (comanda == null)
            {
                Console.WriteLine("Comandă null - nu poate fi onorată.");
                return;
            }

            comanda.Status = "Onorată";
            Console.WriteLine($"✔️ Comanda pentru {comanda.Nume} ({comanda.Email}) a fost onorată de {this.Nume}.");
        }
        public override void ActualizeazaStatusService(CerereService cerere, string statusNou)
        {
            if (cerere == null)
            {
                Console.WriteLine("Cerere null - nu poate fi actualizată.");
                return;
            }

            cerere.Status = statusNou;
            Console.WriteLine($"🔄 Cererea de la {cerere.EmailUtilizator} programată pe" +
                $" {cerere.DataProgramare.ToShortDateString()} a fost actualizată la " +
                $"'{statusNou}' de {this.Nume}.");
        }
    }
}
