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
        public SignUpForm(MainForm mainForm, UserManagerBLL userManagerBLL)
        {
            this.mainForm = mainForm;
            this.Text = "Creare cont";
            this.Size = new Size(300, 280);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userManagerBLL = userManagerBLL;
            int startX = 20;
            int startY = 20;
            int spacingY = 50;

            // Nume
            lblNume = new Label() { Text = "Nume:", Location = new Point(startX, startY), AutoSize = true };
            txtNume = new TextBox() { Location = new Point(startX, startY + 20), Width = 200 };

            // Prenume
            lblPrenume = new Label() { Text = "Prenume:", Location = new Point(startX, startY + spacingY), AutoSize = true };
            txtPrenume = new TextBox() { Location = new Point(startX, startY + spacingY + 20), Width = 200 };

            // Email
            lblEmail = new Label() { Text = "Email:", Location = new Point(startX, startY + spacingY * 2), AutoSize = true };
            txtEmail = new TextBox() { Location = new Point(startX, startY + spacingY * 2 + 20), Width = 200 };

            // Username
            lblUsername = new Label() { Text = "Username:", Location = new Point(startX, startY + spacingY * 3), AutoSize = true };
            txtUsername = new TextBox() { Location = new Point(startX, startY + spacingY * 3 + 20), Width = 200 };

            // Parolă
            lblParola = new Label() { Text = "Parolă:", Location = new Point(startX, startY + spacingY * 4), AutoSize = true };
            txtParola = new TextBox() { Location = new Point(startX, startY + spacingY * 4 + 20), Width = 200 };

            // Adresa
            lblAdresa = new Label() { Text = "Adresa:", Location = new Point(startX, startY + spacingY * 5), AutoSize = true };
            txtAdresa = new TextBox() { Location = new Point(startX, startY + spacingY * 5 + 20), Width = 200 };

            // Telefon
            lblPhone = new Label() { Text = "Număr de telefon:", Location = new Point(startX, startY + spacingY * 6), AutoSize = true };
            txtPhone = new TextBox() { Location = new Point(startX, startY + spacingY * 6 + 20), Width = 200 };

            // Buton
            btnCreeazaCont = new Button() { Text = "Crează cont", Location = new Point(startX + 40, startY + spacingY * 8), Width = 120 };


            btnCreeazaCont.Click += BtnCreeazaCont_ClickAsync;
            this.FormClosing += SignUpForm_FormClosing;
            this.Controls.Add(lblNume);
            this.Controls.Add(txtNume);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblParola);
            this.Controls.Add(txtParola);
            this.Controls.Add(lblPrenume);
            this.Controls.Add(txtPrenume);
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblAdresa);
            this.Controls.Add(txtAdresa);
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);
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
            mainForm.Show();
        }
    }
}
