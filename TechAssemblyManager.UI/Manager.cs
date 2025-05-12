using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public class Manager : MainForm.User
    {
        private MainForm mainForm;
        public Manager(string nume, string email) : base(nume, email)
        {
            this.mainForm = mainForm;
        }

        public void GestioneazaPromotii() {
            var form = new GestioneazaPromotiiForm(mainForm);
            form.ShowDialog();
        }
    }
}
