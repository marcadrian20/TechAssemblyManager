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
        private Button btnCreeazaCont;
        private Label lblNume;
        private Label lblEmail;
        private Label lblParola;
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

            lblEmail = new Label() { Text = "Email:", Location = new Point(20, 60), AutoSize = true };
            txtEmail = new TextBox() { Location = new Point(100, 60), Width = 150 };

            lblParola = new Label() { Text = "Parolă:", Location = new Point(20, 100), AutoSize = true };
            txtParola = new TextBox() { Location = new Point(100, 100), Width = 150 };

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
        private async Task BtnCreeazaCont_ClickAsync(object sender, EventArgs e)
        {
            string nume = txtNume.Text;
            string email = txtEmail.Text;
            string parola = txtParola.Text;

            if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
            {
                MessageBox.Show("Completați toate câmpurile.");
                return;
            }

            bool isSignedUp = await userManagerBLL.RegisterUserAsync(
                            emailTextBox.Text,
                            passwordTextBox.Text,
                            userNameTextBox.Text,
                            firstNameTextBox.Text,
                            lastNameTextBox.Text,
                            addressTextBox.Text,
                            phoneNumberTextBox.Text
                        );

            if (!isSignedUp)
            {
                MessageBox.Show("There was a problem at Sign Up!");
                return;
            }

            // MainForm.User nouUser = new MainForm.User(nume, email);
            // UserManager.AddUser(nouUser, parola); // presupunem că UserManager gestionează parola
            MessageBox.Show("Cont creat cu succes!");
            var user = await userManagerBLL.GetUserByUsernameAsync(userNameTextBox.Text);
            SessionManager.LoggedInUser = user;
            MessageBox.Show("Logare cu succes!");
            this.Hide();
            // new Logare(mainForm).Show();
        }
    }
}
