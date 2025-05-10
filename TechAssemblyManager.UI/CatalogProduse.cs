using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class CatalogProduse : Form
    {
        private FlowLayoutPanel pcComponentsSidebar; // FlowLayoutPanel pentru componentele PC
        private MainForm m;
        private CartForm cartForm;
        private ProductViewerForm prvf;
        private readonly string[] categoriiProduse = { "Toate", "Laptopuri", "Desktopuri", "Monitoare" };

        public object Instance { get; private set; }

        public CatalogProduse(MainForm m)
        {
            InitializeComponent();
            StylizeButtons();
            this.m = m;
            this.Resize += (s, e) => LayoutControls();

            // Inițializare FlowLayoutPanel pentru sidebar-ul cu componentele PC
            pcComponentsSidebar = new FlowLayoutPanel
            {
                Dock = DockStyle.None,
                Width = 200,
                Height = 300, // Poți ajusta dacă vrei
                BackColor = Color.FromArgb(40, 45, 50),
                AutoScroll = true,
                Visible = false,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };
            this.FormClosing += CatalogProduse_FormClosing; // Adaugă evenimentul de închidere
            this.Controls.Add(pcComponentsSidebar);
        }
        private void CatalogProduse_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m != null && !m.IsDisposed)
            {
                m.Show();
            }
        }
        private void InitPcComponentsSidebar()
        {
            string[] componente = { "Procesor", "Placa Video", "Placa de Baza", "RAM", "SSD", "HDD", "Sursa", "Carcasa" };

            pcComponentsSidebar.Controls.Clear(); // Curăță sidebar-ul dacă există deja

            foreach (string componenta in componente)
            {
                Button btn = new Button
                {
                    Text = componenta,
                    Height = 40,
                    Width = pcComponentsSidebar.Width - 20,
                    BackColor = Color.FromArgb(28, 28, 28),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(5)
                };

                btn.Click += (s, e) =>
                {
                    this.Hide();
                    ProductViewerForm pvf = new ProductViewerForm(m, cartForm, componenta);
                    pvf.Show();
                };

                pcComponentsSidebar.Controls.Add(btn);
            }

            // Buton pentru închidere
            Button btnInchide = new Button
            {
                Text = "Închide",
                Height = 40,
                Width = pcComponentsSidebar.Width - 20,
                BackColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5)
            };

            btnInchide.Click += (s, e) =>
            {
                pcComponentsSidebar.Visible = false;
            };

            pcComponentsSidebar.Controls.Add(btnInchide);
        }

        private void LayoutControls()
        {
            int padding = 20;
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Poziționează butoanele mari (centru)
            int buttonWidth = Preasamblate.Width;
            int buttonHeight = Preasamblate.Height;
            int spacing = 30;

            int totalButtonsWidth = buttonWidth * 2 + spacing;
            int startX = (formWidth - totalButtonsWidth) / 2;
            int centerY = formHeight / 2 - buttonHeight;

            Preasamblate.Location = new Point(startX, centerY);
            PiesePC.Location = new Point(startX + buttonWidth + spacing, centerY);

            // Buton „Înapoi” jos centru
            Back.Location = new Point((formWidth - Back.Width) / 2, formHeight - Back.Height - padding);

            // My Account + Cos (dreapta sus)
            Myaccount.Location = new Point(formWidth - Myaccount.Width - padding, padding);
            cos.Location = new Point(Myaccount.Left, Myaccount.Bottom + 5);

            // Sigla/Back sus stânga
            SiglaBtn1.Location = new Point(padding, padding);

            // Label „Catalog” centru sus
            SiglaBtn.Location = new Point((formWidth - SiglaBtn.Width) / 2, padding);

            // Poziționează sidebar-ul (dacă e vizibil)
            if (sidebar.Visible)
            {
                int sidebarX = cos.Left;
                int sidebarY = cos.Bottom + 10;
                sidebar.Location = new Point(sidebarX, sidebarY);
            }

            // Poziționează și sidebar-ul pentru PC components
            if (pcComponentsSidebar.Visible)
            {
                int sidebarX = cos.Left;
                int sidebarY = cos.Bottom + 10;
                pcComponentsSidebar.Location = new Point(sidebarX, sidebarY);
            }
        }

        private void ToggleSidebar()
        {
            sidebar.Visible = !sidebar.Visible;
            LayoutControls();  // This will handle the layout of controls
        }


        private void CatalogProduse_Load(object sender, EventArgs e)
        {
            InitSidebar();
            InitPcComponentsSidebar();
            LayoutControls();
        }

        private void Preasamblate_Click(object sender, EventArgs e)
        {
            ToggleSidebar();
        }

        private void PiesePC_Click(object sender, EventArgs e)
        {
            pcComponentsSidebar.Visible = !pcComponentsSidebar.Visible;
            sidebar.Visible = false; // Hide the main sidebar if necessary
            LayoutControls(); // Recalculate the layout of controls

            // Position the PC Components Sidebar correctly
            int sidebarX = cos.Left;
            int sidebarY = cos.Bottom + 10;
            pcComponentsSidebar.Location = new Point(sidebarX, sidebarY);
        }


        private void StylizeButtons()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = Color.LightGray;
                    btn.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Font mai mare
                    btn.Size = new Size(200, 50); // Dimensiune de 3x mai mare
                }
            }
        }

        private void InitSidebar()
        {
            // Adaugă câte un buton pentru fiecare categorie
            foreach (string categorie in categoriiProduse)
            {
                Button btnCategorie = new Button
                {
                    Text = categorie,
                    Dock = DockStyle.Top,
                    Height = 45,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(28, 28, 28),
                    FlatStyle = FlatStyle.Flat,
                    Tag = categorie // stocăm categoria în Tag ca să o putem accesa în handler
                };

                btnCategorie.Click += (s, e) =>
                {
                    string categorieAleasa = (string)((Button)s).Tag;
                    this.Hide(); // ascundem CatalogProduse
                    ProductViewerForm pvf = new ProductViewerForm(m, cartForm, categorieAleasa); // trimitem categoria
                    pvf.Show();
                };

                sidebar.Controls.Add(btnCategorie);
            }

            // Configurare sidebar principal
            sidebar.Dock = DockStyle.None;
            sidebar.Width = 200;
            sidebar.BackColor = Color.FromArgb(40, 40, 45);
            sidebar.Visible = false;

            // Buton pentru a deschide ProductViewerForm
            Button btnProduse = new Button
            {
                Text = "Catalog Produse",
                Dock = DockStyle.Top,
                Height = 45,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(28, 28, 28),
                FlatStyle = FlatStyle.Flat
            };
            btnProduse.Click += (s, e) =>
            {
                this.Hide(); // ascundem fereastra actuală
                ProductViewerForm pvf = new ProductViewerForm(m, cartForm);
                pvf.Text = "ProductViwerForm";
                pvf.Show();
            };
            sidebar.Controls.Add(btnProduse);

            // Buton pentru închidere sidebar
            Button btnInchide = new Button
            {
                Text = "Închide",
                Dock = DockStyle.Bottom,
                Height = 45,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(64, 64, 64),
                FlatStyle = FlatStyle.Flat
            };
            btnInchide.Click += (s, e) =>
            {
                sidebar.Visible = false;
                MessageBox.Show("Sidebar închis.");
            };
            sidebar.Controls.Add(btnInchide);
        }

        private void MyAccount1_Click(object sender, EventArgs e) { }

        private void Myaccount_Click(object sender, EventArgs e)
        {
            AccountForm accForm = new AccountForm(m, this,prvf);
            accForm.ShowDialog();
            this.Hide();
        }

        private void SiglaBtn1_Click(object sender, EventArgs e)
        {
            m.Show();
            this.Close();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }
        public void AddProdusToCos(Produs produs)
        {
            AppState.AdaugaProdus(produs);
        }
        private void cos_Click(object sender, EventArgs e)
        {
            if (cartForm == null)
            {
                cartForm = new CartForm(m);
            }

            cartForm.SetProduse(AppState.GetProduse());

            cartForm.Show();
            cartForm.BringToFront();
            this.Hide();
        }
    }
}
