using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using static TechAssemblyManager.UI.MainForm;

namespace TechAssemblyManager.UI
{
    public static class UserManager
    {
        private static List<MainForm.User> utilizatori = new List<MainForm.User>();
        private static Dictionary<string, string> parole = new Dictionary<string, string>();

        // Adăugăm un user cu parola corectă
        public static void AddUser(MainForm.User user, string parola)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(parola))
                throw new ArgumentException("Date utilizator invalide.");

            utilizatori.Add(user);
            parole[user.Email] = parola;
        }

        // Metoda pentru a căuta utilizatorul după email și a-l autentifica
        public static MainForm.User AutentificaSiReturneazaUser(string email, string parola)
        {
            if (parole.TryGetValue(email, out string parolaCorecta) && parolaCorecta == parola)
            {
                // Căutăm utilizatorul după email și returnăm tipul corespunzător
                return GetUserByEmail(email);
            }
            return null; // Dacă parola nu este corectă, returnăm null
        }

        // Aici continuăm cu logica pentru a returna utilizatorul de tip corespunzător
        public static MainForm.User GetUserByEmail(string email)
        {
            var user = utilizatori.Find(u => u.Email == email);

            if (user == null)
                return new MainForm.User("Client", email); // Dacă nu găsim un user, creăm unul generic

            // Dacă emailul este unul special, vom returna utilizatorul de tip corect
            if (email == "senior@firma.com")
            {
                return new AngajatSenior(user.Nume ?? "Andrei Senior", email);
            }
            if (email == "junior@firma.com")
            {
                return new AngajatJunior(user.Nume ?? "Andrei Junior", email);
            }
            if (email == "manager@firma.com")
            {
                return new Manager(user.Nume ?? "Andrei Manager", email);
            }

            return user; // Dacă nu este un email special, returnăm userul normal
        }

        // Metoda de autentificare
        public static bool Autentifica(string email, string parola)
        {
            if (parole.TryGetValue(email, out string parolaCorecta))
            {
                return parolaCorecta == parola;
            }
            return false;
        }
    }
}
