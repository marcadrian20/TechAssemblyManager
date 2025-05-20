using System;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class AdaugaAngajatForm : Form
    {
        private TextBox txtNume, txtEmail;
        private ComboBox cmbTipAngajat;
        private Button btnSalveaza;
        private UserManagerBLL userManagerBLL;

        public AdaugaAngajatForm(UserManagerBLL userManagerBLL)
        {
            InitializeComponent();
            this.userManagerBLL = userManagerBLL;
            this.Text = "Adaugă Angajat";
            this.Size = new System.Drawing.Size(300, 300);

            // Nume
            Label lblNume = new Label { Text = "Nume:", Top = 20, Left = 20 };
            txtNume = new TextBox { Top = 40, Left = 20, Width = 240 };
            this.Controls.Add(lblNume);
            this.Controls.Add(txtNume);

            // Email
            Label lblEmail = new Label { Text = "Email:", Top = 80, Left = 20 };
            txtEmail = new TextBox { Top = 100, Left = 20, Width = 240 };
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);

            // Tip Angajat
            Label lblTipAngajat = new Label { Text = "Tip Angajat:", Top = 140, Left = 20 };
            cmbTipAngajat = new ComboBox { Top = 160, Left = 20, Width = 240 };
            cmbTipAngajat.Items.AddRange(new string[] { "Junior", "Senior" });
            this.Controls.Add(lblTipAngajat);
            this.Controls.Add(cmbTipAngajat);

            // Salvează
            btnSalveaza = new Button
            {
                Text = "Salvează",
                Top = 200,
                Left = 20,
                Width = 240
            };
            btnSalveaza.Click += BtnSalveaza_Click;
            this.Controls.Add(btnSalveaza);
        }

        private async void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNume.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || cmbTipAngajat.SelectedItem == null)
            {
                MessageBox.Show("Completați toate câmpurile.");
                return;
            }

            var user = new User
            {
                userName = txtNume.Text.Trim().Replace(" ", "").ToLower(),
                email = txtEmail.Text.Trim(),
                userType = "employee",
                employeeData = new EmployeeData
                {
                    isSenior = cmbTipAngajat.SelectedItem.ToString() == "Senior"
                }
            };

            // You may want to prompt for a password or generate one
            string password = "defaultPassword123"; // Replace with real logic

            bool result = await userManagerBLL.RegisterUserAsync(user, password);
            if (result)
            {
                MessageBox.Show("Angajat adăugat cu succes!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Eroare: utilizatorul există deja sau date invalide.");
            }
        }
    }
}