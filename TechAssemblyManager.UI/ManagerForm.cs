using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class ManagerForm: Form
    {
        private ListBox listBox1;
        private Button btnAdaugaAngajat;
        private Button delete;
        public MainForm main;
        public UserManagerBLL userManagerBLL;
        private ManagerForm Instance { get; set; }

        public ManagerForm()
        {
            InitializeComponent();
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
        private async Task IncarcaAngajati()
        {
            listBox1.Items.Clear();
            var angajati = await userManagerBLL.GetAllEmployeesAsync();
            if (angajati == null)
            {
                listBox1.Items.Add("Nu există angajați.");
                return;
            }
            foreach (var angajat in angajati)
            {
                Console.WriteLine($"Loaded employee: {angajat.firstName} ({angajat.email})");
                listBox1.Items.Add(angajat);
            }
        }

        private void BtnAdaugaAngajat_Click(object sender, EventArgs e)
        {
            var form = new AdaugaAngajatForm(userManagerBLL);
            form.FormClosed += (s, args) => IncarcaAngajati(); // Refresh the list after adding  
            form.ShowDialog();
        }
        private async void delete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var angajat = listBox1.SelectedItem as User;
                if (angajat != null)
                {
                    await userManagerBLL.DeleteUserAsync(angajat.userName);
                }
                await IncarcaAngajati();
            }
        }
    }
}
