using System;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class CerereServiceForm : Form
    {
        public CerereServiceForm()
        {
            InitializeComponent();
        }

        private void BtnTrimite_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) ||
                string.IsNullOrWhiteSpace(txtDescriere.Text))
            {
                MessageBox.Show("Te rugăm să completezi toate câmpurile!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Cererea a fost trimisă cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Deschide MainForm și închide formularul curent
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }
    }
}
