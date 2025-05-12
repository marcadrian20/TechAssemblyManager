using System;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class CerereServiceForm : Form
    {
        private DateTimePicker dtpData;
        private MainForm mainForm;
        private TextBox txtNume;
        private TextBox txtEmail;
        private TextBox txtParola;
        public CerereServiceForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.Text = "Trimite cerere service";
            this.Size = new System.Drawing.Size(400, 250);

            Label lblData = new Label { Text = "Data programării:", Top = 20, Left = 20 };
            dtpData = new DateTimePicker { Top = 40, Left = 20, Width = 300 };

            Label lblDescriere = new Label { Text = "Descriere problemă:", Top = 80, Left = 20 };
            txtDescriere = new TextBox { Top = 100, Left = 20, Width = 300, Height = 60, Multiline = true };
            Label Nume = new Label { Text = "Nume:", Top = 170, Left = 20 };
            txtNume = new TextBox { Top = 190, Left = 20, Width = 300, Height = 20 };
            Label Email = new Label { Text = "Email:", Top = 220, Left = 20 };
            txtEmail = new TextBox { Top = 240, Left = 20, Width = 300, Height = 20 };
            Label Parola = new Label { Text = "Parolă:", Top = 270, Left = 20 };
            txtParola = new TextBox { Top = 290, Left = 20, Width = 300, Height = 20 };
            btnTrimite = new Button { Text = "Trimite cerere", Top = 320, Left = 20 };
            btnTrimite.Click += BtnTrimite_Click;
            txtNume.Click += txtNume_Click;
            txtEmail.Click += txtEmail_Click;
            txtParola.Click += txtParola_Click;
            this.FormClosing += CerereServiceForm_FormClosing;
            this.Controls.AddRange(new Control[] { lblData, dtpData, lblDescriere, txtDescriere,Nume,txtNume,Email,txtEmail,Parola,txtParola, btnTrimite });
        }
        private void CerereServiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
         if(mainForm != null  && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        public CerereServiceForm(MainForm.User user)
        {
            this.Text = "Trimite cerere service";
            this.Size = new System.Drawing.Size(400, 250);

            Label lblData = new Label { Text = "Data programării:", Top = 20, Left = 20 };
            dtpData = new DateTimePicker { Top = 40, Left = 20, Width = 300 };

            Label lblDescriere = new Label { Text = "Descriere problemă:", Top = 80, Left = 20 };
            txtDescriere = new TextBox { Top = 100, Left = 20, Width = 300, Height = 60, Multiline = true };

            btnTrimite = new Button { Text = "Trimite cerere", Top = 180, Left = 20 };
            btnTrimite.Click += BtnTrimite_Click;

            this.Controls.AddRange(new Control[] { lblData, dtpData, lblDescriere, txtDescriere, btnTrimite });
        }
        private void txtNume_Click(object sender, EventArgs e)
        {
            if (txtNume.Equals(null))
                MessageBox.Show("Completează numele.");
        }
        private void txtEmail_Click(object sender, EventArgs e)
        {
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Emailul trebuie să conțină caracterul '@'.");
            }
        }
        private void txtParola_Click(object sender, EventArgs e)
        {
            string parola = txtParola.Text;

            if (parola.Length < 6)
            {
                MessageBox.Show("Parola trebuie să aibă cel puțin 6 caractere.");
            }
            else if (!parola.Any(char.IsDigit))
            {
                MessageBox.Show("Parola trebuie să conțină cel puțin o cifră.");
            }
            else if (!parola.Any(char.IsUpper))
            {
                MessageBox.Show("Parola trebuie să conțină cel puțin o literă mare.");
            }
        }
        private void BtnTrimite_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescriere.Text))
            {
                MessageBox.Show("Completează descrierea.");
                return;
            }
            if(string.IsNullOrWhiteSpace(txtNume.Text))
            {
                MessageBox.Show("Completează numele.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Emailul trebuie să conțină caracterul '@'.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtParola.Text) || txtParola.Text.Length < 6)
            {
                MessageBox.Show("Parola trebuie să aibă cel puțin 6 caractere.");
                return;
            }
            else if (!txtParola.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Parola trebuie să conțină cel puțin o cifră.");
                return;
            }
            else if (!txtParola.Text.Any(char.IsUpper))
            {
                MessageBox.Show("Parola trebuie să conțină cel puțin o literă mare.");
                return;
            }
            if (AppState.UtilizatorCurent == null)
            {
                MessageBox.Show("Trebuie să fii autentificat.");
                return;
            }

            var cerere = new CerereService
            {
                Descriere = txtDescriere.Text,
                DataProgramare = dtpData.Value.Date,
                EmailUtilizator = AppState.UtilizatorCurent.Email
            };

            AppState.AdaugaCerereService(cerere);
            MessageBox.Show("Cererea a fost trimisă.");
            this.Close();
        }
    }
}
