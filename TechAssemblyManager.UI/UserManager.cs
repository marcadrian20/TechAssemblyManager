using System;
using System.Collections.Generic;

namespace TechAssemblyManager.UI
{
    public static class UserManager
    {
        private static List<MainForm.User> utilizatori = new List<MainForm.User>();
        private static Dictionary<string, string> parole = new Dictionary<string, string>();

        public static void AddUser(MainForm.User user, string parola)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(parola))
                throw new ArgumentException("Date utilizator invalide.");

            utilizatori.Add(user);
            parole[user.Email] = parola;
        }

        public static MainForm.User GetUserByEmail(string email)
        {
            return utilizatori.Find(u => u.Email == email);
        }

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
