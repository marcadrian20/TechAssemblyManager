using System;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class AdaugaProdusForm : Form
    {
        private TextBox txtNume, txtPret, txtDescriere;
        private ComboBox cmbCategorie, cmbScor;
        private Button btnAdauga;
        private ProductManagerBLL productManagerBLL;
        private User currentUser;

        public AdaugaProdusForm(ProductManagerBLL productManagerBLL, User currentUser)
        {
            InitializeComponent();
            this.productManagerBLL = productManagerBLL;
            this.currentUser = currentUser;
            this.Text = "Adaugă Produs Nou";
            this.Size = new System.Drawing.Size(400, 400);

            // Nume
            Label lblNume = new Label() { Text = "Nume:", Top = 20, Left = 20 };
            txtNume = new TextBox() { Top = 40, Left = 20, Width = 300 };

            // Pret
            Label lblPret = new Label() { Text = "Preț:", Top = 70, Left = 20 };
            txtPret = new TextBox() { Top = 90, Left = 20, Width = 300 };

            // Descriere
            Label lblDescriere = new Label() { Text = "Descriere:", Top = 120, Left = 20 };
            txtDescriere = new TextBox() { Top = 140, Left = 20, Width = 300, Height = 60, Multiline = true };

            // Categorie
            Label lblCategorie = new Label() { Text = "Categorie:", Top = 210, Left = 20 };
            cmbCategorie = new ComboBox() { Top = 230, Left = 20, Width = 300 };
            cmbCategorie.Items.AddRange(new string[] {
                "Desktop PC", "Laptop PC", "Imprimantă", "Periferic", "Componentă"
            });

            // Scor
            Label lblScor = new Label() { Text = "Scor Critici (1-5):", Top = 260, Left = 20 };
            cmbScor = new ComboBox() { Top = 280, Left = 20, Width = 300 };
            cmbScor.Items.AddRange(new string[] { "1", "2", "3", "4", "5" });

            // Buton
            btnAdauga = new Button()
            {
                Text = "Adaugă produs",
                Top = 320,
                Left = 20,
                Width = 300
            };
            btnAdauga.Click += BtnAdauga_Click;
            this.FormClosing += AdaugaProdusForm_FormClosing;
            this.Controls.AddRange(new Control[] {
                lblNume, txtNume,
                lblPret, txtPret,
                lblDescriere, txtDescriere,
                lblCategorie, cmbCategorie,
                lblScor, cmbScor,
                btnAdauga
            });
        }

        private void AdaugaProdusForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Optionally, refresh product list in parent form
        }

        private async void BtnAdauga_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNume.Text) ||
                string.IsNullOrWhiteSpace(txtPret.Text) ||
                string.IsNullOrWhiteSpace(txtDescriere.Text) ||
                cmbCategorie.SelectedItem == null ||
                cmbScor.SelectedItem == null)
            {
                MessageBox.Show("Completați toate câmpurile.");
                return;
            }

            if (!float.TryParse(txtPret.Text, out float pret))
            {
                MessageBox.Show("Prețul trebuie să fie un număr valid.");
                return;
            }

            var product = new Product
            {
                productId = Guid.NewGuid().ToString(),
                name = txtNume.Text.Trim(),
                price = pret,
                description = txtDescriere.Text.Trim(),
                categoryId = cmbCategorie.SelectedItem.ToString(),
                rating = int.Parse(cmbScor.SelectedItem.ToString()),
                isActive = true
            };

            bool result = await productManagerBLL.AddProductAsync(product, currentUser);
            if (result)
            {
                MessageBox.Show("Produs adăugat cu succes!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Eroare la adăugarea produsului (verificați permisiunile sau datele).");
            }
        }
    }
}