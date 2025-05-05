using System;
using System.Windows.Forms;
using System.Drawing;

namespace TechAssemblyManager.UI
{
    public partial class Logare : Form
    {
        private TextBox txtEmail;
        private TextBox txtParola;
        private Button btnSignIn;
        private Button btnSignUp;
        private Label lblEmail;
        private Label lblParola;

        public Logare()
        {
            this.Text = "Autentificare";
            this.Size = new Size(300, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblEmail = new Label() { Text = "Email:", Location = new Point(20, 20), AutoSize = true };
            txtEmail = new TextBox() { Location = new Point(100, 20), Width = 150 };

            lblParola = new Label() { Text = "Parolă:", Location = new Point(20, 60), AutoSize = true };
            txtParola = new TextBox() { Location = new Point(100, 60), Width = 150, UseSystemPasswordChar = true };

            btnSignIn = new Button() { Text = "Sign In", Location = new Point(40, 120), Width = 90 };
            btnSignUp = new Button() { Text = "Sign Up", Location = new Point(150, 120), Width = 90 };

            btnSignIn.Click += BtnSignIn_Click;
            btnSignUp.Click += BtnSignUp_Click;

            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblParola);
            this.Controls.Add(txtParola);
            this.Controls.Add(btnSignIn);
            this.Controls.Add(btnSignUp);
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string parola = txtParola.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
            {
                MessageBox.Show("Completați toate câmpurile.");
                return;
            }

            // Aici se face autentificarea
            MainForm.User user = UserManager.GetUserByEmail(email);
            if (user != null && user.Autentifica(parola))
            {
                MessageBox.Show("Autentificat cu succes!");
                AppState.UtilizatorCurent = user;
                this.Hide();
                new MainForm().Show();
            }
            else
            {
                MessageBox.Show("Email sau parolă incorectă.");
            }
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SignUpForm().Show();
        }
    }
}
