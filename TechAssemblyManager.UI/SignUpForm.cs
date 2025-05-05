using System;
using System.Windows.Forms;
using System.Drawing;

namespace TechAssemblyManager.UI
{
    public partial class SignUpForm : Form
    {
        private TextBox txtNume;
        private TextBox txtEmail;
        private TextBox txtParola;
        private Button btnCreeazaCont;
        private Label lblNume;
        private Label lblEmail;
        private Label lblParola;

        public SignUpForm()
        {
            this.Text = "Creare cont";
            this.Size = new Size(300, 280);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblNume = new Label() { Text = "Nume:", Location = new Point(20, 20), AutoSize = true };
            txtNume = new TextBox() { Location = new Point(100, 20), Width = 150 };

            lblEmail = new Label() { Text = "Email:", Location = new Point(20, 60), AutoSize = true };
            txtEmail = new TextBox() { Location = new Point(100, 60), Width = 150 };

            lblParola = new Label() { Text = "Parolă:", Location = new Point(20, 100), AutoSize = true };
            txtParola = new TextBox() { Location = new Point(100, 100), Width = 150 };

            btnCreeazaCont = new Button() { Text = "Crează cont", Location = new Point(100, 150), Width = 100 };
            btnCreeazaCont.Click += BtnCreeazaCont_Click;

            this.Controls.Add(lblNume);
            this.Controls.Add(txtNume);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblParola);
            this.Controls.Add(txtParola);
            this.Controls.Add(btnCreeazaCont);
        }

        private void BtnCreeazaCont_Click(object sender, EventArgs e)
        {
            string nume = txtNume.Text;
            string email = txtEmail.Text;
            string parola = txtParola.Text;

            if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
            {
                MessageBox.Show("Completați toate câmpurile.");
                return;
            }

            MainForm.User nouUser = new MainForm.User(nume, email);
            UserManager.AddUser(nouUser, parola); // presupunem că UserManager gestionează parola
            MessageBox.Show("Cont creat cu succes!");

            this.Hide();
            new Logare().Show();
        }
    }
}
