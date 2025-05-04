using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class MainForm : Form
    {
        PromotiiForm promotiiForm;
        public CartForm cartForm;
        ProductViewerForm prvf;
        MainForm mainForm;
        Form f;
        private List<Produs> produseInCos = new List<Produs>();
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            prvf = new ProductViewerForm(this, null);
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int centerX = (screen.Width - TechAssemblyManager.Width) / 2;
            int centerY = (screen.Height - TechAssemblyManager.Height) / 2;
            TechAssemblyManager.Location = new Point(centerX, centerY);
            Instance = this;
        }

        public MainForm(MainForm instance)
        {
            Instance = instance;
        }

        private void ViewCatalog_Click(object sender, EventArgs e)
        {
            CatalogProduse c = new CatalogProduse(this.Instance);
            c.Show();
            this.Hide();
        }

        private void Promotii_Click(object sender, EventArgs e)
        {
            promotiiForm = new PromotiiForm();
            promotiiForm.Show();
            this.Hide();
        }

        private void Myaccount_Click(object sender, EventArgs e)
        {
            AccountForm accountForm1 = new AccountForm(this, this);
            accountForm1.Show();
            this.Hide();
        }

        public void AddProdusToCos(Produs produs)
        {
            AppState.AdaugaProdus(produs);
        }

        private void cos_Click(object sender, EventArgs e)
        {
            if (cartForm == null)
            {
                cartForm = new CartForm(this, prvf);
            }

            cartForm.SetProduse(AppState.GetProduse());

            cartForm.Show();
            cartForm.BringToFront();
            this.Hide();
        }

        private void AcceseazaProduseDinCos()
        {
            if (cartForm != null)
            {
                List<Produs> produse = cartForm.GetProduse();
                foreach (var produs in produse)
                {
                    Console.WriteLine($"Produs: {produs.Nume}, Preț: {produs.Pret}");
                }
            }
        }

        private void Searchbar_TextChanged(object sender, EventArgs e) { }
        private void TechAssemblyManager_Enter(object sender, EventArgs e) { }
        private void CerereService_Click(object sender, EventArgs e)
        {
            CerereServiceForm cerereForm = new CerereServiceForm();
            cerereForm.ShowDialog();
        }

        public MainForm Instance { get; }
    }
}
