using System;
using System.Windows.Forms;
using System.Drawing;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;

namespace TechAssemblyManager.UI
{
    public partial class SignUpForm : Form
    {
        private TextBox txtNume;
        private TextBox txtEmail;
        private TextBox txtParola;
        private TextBox txtPrenume;
        private TextBox txtUsername;
        private TextBox txtAdresa;
        private TextBox txtPhone;
        private Button btnCreeazaCont;
        private Label lblNume;
        private Label lblEmail;
        private Label lblParola;

        private Label lblPrenume;
        private Label lblUsername;
        private Label lblAdresa;
        private Label lblPhone;
        private MainForm mainForm;
        private UserManagerBLL userManagerBLL;
        public SignUpForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.Text = "Creare cont";
            this.Size = new Size(300, 280);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblNume = new Label() { Text = "Nume:", Location = new Point(20, 20), AutoSize = true };
            txtNume = new TextBox() { Location = new Point(100, 20), Width = 150 };

            lblPrenume = new Label() { Text = "Prenume:", Location = new Point(20, 20), AutoSize = true };
            txtPrenume = new TextBox() { Location = new Point(100, 20), Width = 150 };

            lblEmail = new Label() { Text = "Email:", Location = new Point(20, 60), AutoSize = true };
            txtEmail = new TextBox() { Location = new Point(100, 60), Width = 150 };

            lblUsername = new Label() { Text = "Username:", Location = new Point(20, 60), AutoSize = true };
            txtUsername = new TextBox() { Location = new Point(100, 60), Width = 150 };

            lblParola = new Label() { Text = "Parolă:", Location = new Point(20, 100), AutoSize = true };
            txtParola = new TextBox() { Location = new Point(100, 100), Width = 150 };

            lblAdresa = new Label() { Text = "Adresa:", Location = new Point(20, 100), AutoSize = true };
            txtAdresa = new TextBox() { Location = new Point(100, 100), Width = 150 };

            lblPhone = new Label() { Text = "Numar de telefon:", Location = new Point(20, 100), AutoSize = true };
            txtPhone = new TextBox() { Location = new Point(100, 100), Width = 150 };

            btnCreeazaCont = new Button() { Text = "Crează cont", Location = new Point(100, 150), Width = 100 };
            btnCreeazaCont.Click += BtnCreeazaCont_ClickAsync;
            this.FormClosing += SignUpForm_FormClosing;
            this.Controls.Add(lblNume);
            this.Controls.Add(txtNume);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblParola);
            this.Controls.Add(txtParola);
            this.Controls.Add(btnCreeazaCont);
        }
        private void SignUpForm_FormClosing(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        private async void BtnCreeazaCont_ClickAsync(object sender, EventArgs e)
        {
            // string nume = txtNume.Text;

            // string email = txtEmail.Text;
            // string parola = txtParola.Text;

            if (string.IsNullOrWhiteSpace(txtNume.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtParola.Text))
            {
                MessageBox.Show("Completați toate câmpurile.");
                return;
            }

            bool isSignedUp = await userManagerBLL.RegisterUserAsync(
                            txtEmail.Text,
                            txtParola.Text,
                            txtUsername.Text,
                            txtNume.Text,
                            txtPrenume.Text,
                            txtAdresa.Text,
                            txtPhone.Text
                        );

            if (!isSignedUp)
            {
                MessageBox.Show("There was a problem at Sign Up!");
                return;
            }

            // MainForm.User nouUser = new MainForm.User(nume, email);
            // UserManager.AddUser(nouUser, parola); // presupunem că UserManager gestionează parola
            MessageBox.Show("Cont creat cu succes!");
            var user = await userManagerBLL.GetUserByUsernameAsync(txtUsername.Text);
            SessionManager.LoggedInUser = user;
            MessageBox.Show("Logare cu succes!");
            this.Hide();
            // new Logare(mainForm).Show();
        }
    }
}
