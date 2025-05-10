using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class ManagerForm: Form
    {
        private ListBox listBox1;
        private Button btnAdaugaAngajat;
        private Button delete;
        public MainForm main;
        private ManagerForm Instance { get; set; }

        public ManagerForm()
        {
            InitializeComponent();
            AppState.AdaugaAngajat(new JuniorAngajat("Ion Popescu", "ion.popescu@example.com"));
            AppState.AdaugaAngajat(new SeniorAngajat("Maria Ionescu", "maria.ionescu@example.com"));
            this.main = main;
            Instance = this;
            this.Text = "Manager - Gestionare Angajați";
            this.Size = new System.Drawing.Size(400, 400);

            // ListBox for displaying employees  
            listBox1 = new ListBox
            {
                Top = 20,
                Left = 20,
                Width = 340,
                Height = 200
            };
            this.Controls.Add(listBox1);

            // Button to add a new employee  
            btnAdaugaAngajat = new Button
            {
                Text = "Adaugă Angajat",
                Top = 240,
                Left = 20,
                Width = 340
            };
            btnAdaugaAngajat.Click += BtnAdaugaAngajat_Click;
            this.Controls.Add(btnAdaugaAngajat);
            delete = new Button
            {
                Text = "Șterge Angajat",
                Top = 280,
                Left = 20,
                Width = 340
            };
            delete.Click += delete_Click;
            this.Controls.Add(delete);
            // Load existing employees  
            IncarcaAngajati();
            this.main = main;

        }
        private void IncarcaAngajati()
        {
            listBox1.Items.Clear();
            var angajati = AppState.GetAngajati();
            if (angajati == null || angajati.Count == 0)
            {
                listBox1.Items.Add("Nu există angajați.");
                return;
            }
            foreach (var angajat in angajati)
            {
                Console.WriteLine($"Loaded employee: {angajat.Nume} ({angajat.Email})");
                listBox1.Items.Add(angajat);
            }
        }

        private void BtnAdaugaAngajat_Click(object sender, EventArgs e)
        {
            var form = new AdaugaAngajatForm();
            form.FormClosed += (s, args) => IncarcaAngajati(); // Refresh the list after adding  
            form.ShowDialog();
        }
        private void delete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var angajat = listBox1.SelectedItem as Angajat;
                if (angajat != null)
                {
                    AppState.StergeAngajat(angajat);
                }
                IncarcaAngajati();
            }
        }
    }
}
