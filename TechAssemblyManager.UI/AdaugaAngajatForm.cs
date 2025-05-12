using System;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class AdaugaAngajatForm : Form
    {
        private TextBox txtNume, txtEmail;
        private ComboBox cmbTipAngajat;
        private Button btnSalveaza;
        private TextBox tipcont;
        public AdaugaAngajatForm()
        {
            InitializeComponent();
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

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNume.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || cmbTipAngajat.SelectedItem == null)
            {
                MessageBox.Show("Completați toate câmpurile.");
                return;
            }

            Angajat angajat;
            if (cmbTipAngajat.SelectedItem.ToString() == "Junior")
            {
                angajat = new JuniorAngajat(txtNume.Text, txtEmail.Text);
            }
            else
            {
                angajat = new SeniorAngajat(txtNume.Text, txtEmail.Text);
            }

            AppState.AdaugaAngajat(angajat);
            Console.WriteLine($"Added employee: {angajat.Nume} ({angajat.Email})");
            MessageBox.Show("Angajat adăugat cu succes!");
            this.Close();
        }
    }

    // Concrete classes for Angajat
    public class JuniorAngajat : Angajat
    {
        public JuniorAngajat(string nume, string email) : base(nume, email)
        {
        }

        public override void OnoreazaComanda(Comanda comanda)
        {
            // Implementation for JuniorAngajat
        }

        public override void ActualizeazaStatusService(CerereService cerere, string statusNou)
        {
            // Implementation for JuniorAngajat
        }
    }

    public class SeniorAngajat : Angajat
    {
        public SeniorAngajat(string nume, string email) : base(nume, email) // Call the base constructor
        {

        }

        public override void OnoreazaComanda(Comanda comanda)
        {
            // Implementation for SeniorAngajat
        }

        public override void ActualizeazaStatusService(CerereService cerere, string statusNou)
        {
            // Implementation for SeniorAngajat
        }
    }
}