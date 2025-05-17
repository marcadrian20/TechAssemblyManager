using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class OnorareComenziForm : Form
    {
        private ListBox lstComenzi;
        private Button btnModificaStatus;
        private ComboBox cmbStatusNou;
        private Label lblStatusNou;
        private MainForm mainForm;
        public CartForm cartForm;
        private ProductViewerForm prvf;
        private AccountForm accountForm;
        public OnorareComenziForm(MainForm.User user,MainForm mainForm)
        {
            if (mainForm == null || mainForm.Instance == null)
            {
                throw new ArgumentNullException(nameof(mainForm), "MainForm or its Instance cannot be null.");
            }
            this.mainForm = mainForm.Instance;
            this.accountForm = accountForm;
            this.prvf = new ProductViewerForm(this);
            this.Text = "Onorare Comenzi";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            lstComenzi = new ListBox() { Width = 450, Height = 200, Top = 20, Left = 20 };
            lblStatusNou = new Label() { Text = "Status nou:", Top = 240, Left = 20 };
            cmbStatusNou = new ComboBox() { Top = 260, Left = 20, Width = 200 };
            cmbStatusNou.Items.AddRange(new string[] { "În așteptare", "În curs", "Finalizată" });

            btnModificaStatus = new Button() { Text = "Actualizează Status", Top = 300, Left = 20, Width = 200 };
            btnModificaStatus.Click += BtnModificaStatus_Click;
            this.Controls.Add(lstComenzi);
            this.Controls.Add(lblStatusNou);
            this.Controls.Add(cmbStatusNou);
            this.Controls.Add(btnModificaStatus);
            this.FormClosing += OnorareComenziForm_FormClosing;
            IncarcaComenzi();
        }
        private void OnorareComenziForm_FormClosing(object sender, EventArgs e)
        {
            if(mainForm != null  && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        private void IncarcaComenzi()
        {
            if (cartForm == null)
            {
                cartForm = new CartForm(mainForm,prvf);
            }
            cartForm.SetProduse(AppState.GetProduse());
            if (cartForm != null)
            {
                List<Produs> produse = cartForm.GetProduse();
                foreach (var produs in produse)
                {
                    lstComenzi.Items.Add($"Produs: {produs.Nume}, Preț: {produs.Pret}");
                }
            }
        }

        private void BtnModificaStatus_Click(object sender, EventArgs e)
        {
            if (lstComenzi.SelectedIndex == -1)
            {
                MessageBox.Show("Selectează o comandă.");
                return;
            }

            string statusNou = cmbStatusNou.SelectedItem as string;
            if (string.IsNullOrEmpty(statusNou))
            {
                MessageBox.Show("Selectează un status nou.");
                return;
            }
            IncarcaComenzi();
            // Aici actualizezi statusul în obiectul real
            MessageBox.Show("Status actualizat la: " + statusNou);
        }
    }
}

