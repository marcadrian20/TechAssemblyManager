using System.Collections.Generic;
using TechAssemblyManager.UI;

namespace TechAssemblyManager
{
    public static class AppState
    {
        private static List<Produs> produseInCos = new List<Produs>();

        public static void AdaugaProdus(Produs produs)
        {
            produseInCos.Add(produs);
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
        }
    }
}
