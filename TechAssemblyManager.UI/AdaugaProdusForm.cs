using System;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class AdaugaProdusForm : Form
    {
        private TextBox txtNume, txtPret, txtDescriere;
        private ComboBox cmbCategorie, cmbScor;
        private Button btnAdauga;
        private ProductViewerForm productViewerForm;
        private ProductViewerForm _productViewerForm;
        private MainForm mainForm;
        public AdaugaProdusForm(ProductViewerForm productViewerForm,MainForm mainForm)
        {
            if (mainForm == null || mainForm.Instance == null)
            {
                throw new ArgumentNullException(nameof(mainForm), "MainForm or its Instance cannot be null.");
            }
            this.mainForm = mainForm.Instance;
            InitializeComponent();
            _productViewerForm = productViewerForm;
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
            this.FormClosing += new FormClosingEventHandler(AdaugaProdusForm_FormClosing); // Explicitly specify the delegate
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
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        private void BtnAdauga_Click(object sender, EventArgs e)
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

            if (!decimal.TryParse(txtPret.Text, out decimal pret))
            {
                MessageBox.Show("Prețul trebuie să fie un număr valid.");
                return;
            }
            if (_productViewerForm == null)
            {
                _productViewerForm=new ProductViewerForm(this);
                return;
            }

            Produs produs = new Produs
            {
                Nume = txtNume.Text,
                Pret = pret,
                Descriere = $"{cmbCategorie.SelectedItem} - {txtDescriere.Text.Trim()}",
                ScorCritici = int.Parse(cmbScor.SelectedItem.ToString()),
                Categorie = cmbCategorie.SelectedItem.ToString()
            };
            _productViewerForm.AdaugaProdus(produs);    
            AppState.AdaugaProdus(produs);
            _productViewerForm.ReincarcaProduse();
            MessageBox.Show("Produs adăugat cu succes!");
            this.Close();
        }
    }
}
