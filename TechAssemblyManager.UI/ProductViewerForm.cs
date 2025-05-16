using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class ProductViewerForm : Form
    {
        private MainForm mainForm;
        public CartForm cartForm;
        private List<Produs> toateProdusele = new List<Produs>();
        private List<Produs> produseFiltrate = new List<Produs>();
        private List<Produs> cosCumparaturi = new List<Produs>();
        private Dictionary<Produs, int> ratinguri = new Dictionary<Produs, int>();
        private int produsePePagina = 10;
        private int paginaCurenta = 1;

        private FlowLayoutPanel flpProduse;
        private FlowLayoutPanel flpPaginare;
        private TextBox txtCautare;
        private Button btnCauta;
        private ListBox lstCos;
        private Label lblCos;
        private ComboBox cmbCategorii;
        private Button btnAccount;
        private object instance;

        public ProductViewerForm Instance { get; set; }

        public ProductViewerForm(MainForm mainForm, CartForm cartForm, string categorieInitiala = null)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.cartForm = cartForm;
            Instance = this;
            this.Text = "Catalog Produse";
            this.Size = new Size(1000, 700);
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Font = new Font("Segoe UI", 10);

            InitializeLayout();
            LoadProduse();

            cmbCategorii.SelectedIndexChanged -= cmbCategorii_SelectedIndexChanged;
            cmbCategorii.Items.Clear();
            string[] categorii = { "Toate", "Laptopuri", "Desktopuri", "Monitoare" };
            cmbCategorii.Items.AddRange(categorii);

            if (!string.IsNullOrEmpty(categorieInitiala) && cmbCategorii.Items.Contains(categorieInitiala))
                cmbCategorii.SelectedItem = categorieInitiala;
            else
                cmbCategorii.SelectedIndex = 0;

            cmbCategorii.SelectedIndexChanged += cmbCategorii_SelectedIndexChanged;
            this.FormClosing += ProductViewerForm_FormClosing;
            FiltreazaProduse();
            AfiseazaPagina(1);
        }
        private void ProductViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        public ProductViewerForm(object instance)
        {
            InitializeComponent();
            this.instance = instance;

            // Ensure layout is initialized
            InitializeLayout();
        }

        private void ProductViewerForm_Load(object sender, EventArgs e)
        {
            FiltreazaProduse();
            AfiseazaPagina(1);
        }
        public ProductViewerForm getInstance()
        {
            return this;
        }

        private void InitializeLayout()
        {
            int offsetDreapta = 280; // cât să mutăm în dreapta

            txtCautare = new TextBox { Width = 200, Left = 10 + offsetDreapta, Top = 10 };
            btnCauta = new Button
            {
                Text = "Caută",
                Left = 220 + offsetDreapta,
                Top = 10,
                Width = 80,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCauta.FlatAppearance.BorderSize = 0;
            btnCauta.Click += (s, e) =>
            {
                paginaCurenta = 1;
                FiltreazaProduse();
                AfiseazaPagina(paginaCurenta);
            };
            this.Controls.Add(txtCautare);
            this.Controls.Add(btnCauta);

            cmbCategorii = new ComboBox
            {
                Location = new Point(320 + offsetDreapta, 10),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategorii.SelectedIndexChanged += cmbCategorii_SelectedIndexChanged;
            this.Controls.Add(cmbCategorii);

            flpProduse = new FlowLayoutPanel
            {
                Location = new Point(10 + offsetDreapta, 50),
                Size = new Size(700, 550),
                AutoScroll = true,
                WrapContents = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(flpProduse);
            Button btnReincarca = new Button
            {
                Text = "Reîncarcă Produse",
                Location = new Point(500, 10), // Adaptează locația butonului
                Width = 120,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnReincarca.Click += (s, e) =>
            {
                ReincarcaProduse(); // Apelăm funcția de reîncărcare
            };
            this.Controls.Add(btnReincarca);
            flpPaginare = new FlowLayoutPanel
            {
                Location = new Point(10 + offsetDreapta, 610),
                Size = new Size(700, 40),
                FlowDirection = FlowDirection.LeftToRight
            };
            this.Controls.Add(flpPaginare);
            lblPagina = new Label
            {
                Location = new Point(580 + offsetDreapta, 655),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic)
            };
            this.Controls.Add(lblPagina);

            lblCos = new Label
            {
                Text = "Coș cumpărături",
                Location = new Point(730 + offsetDreapta, 10),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true
            };
            lstCos = new ListBox
            {
                Location = new Point(730 + offsetDreapta, 40),
                Size = new Size(240, 500)
            };
            this.Controls.Add(lblCos);
            this.Controls.Add(lstCos);

            btnAccount = new Button
            {
                Text = "Contul meu",
                Location = new Point(730 + offsetDreapta, 560),
                Size = new Size(240, 30)
            };
            btnAccount.Click += Account_Click;
            this.Controls.Add(btnAccount);

            this.Load += ProductViewerForm_Load;
        }

        private void LoadProduse()
        {
            string[] categorii = { "Toate", "Laptopuri", "Desktopuri", "Monitoare", "Procesor", "Placa Video", "Placa de Baza", "RAM", "SSD", "HDD", "Sursa", "Carcasa", "Imprimante", "Mouse", "Tastatura" };
            cmbCategorii.Items.Clear();
            cmbCategorii.Items.AddRange(categorii);
            cmbCategorii.SelectedIndex = 0;

            Random rand = new Random();

            for (int i = 1; i <= 100; i++)
            {
                string categorieRandom = categorii[rand.Next(1, categorii.Length)];
                var produs = new Produs
                {
                    Nume = $"{categorieRandom} {i}",
                    Pret = 100 + rand.Next(0, 5000),
                    Imagine = Properties.Resources.gaming_zmeu_max_amd_ryzen_3_3200g_36ghz_16gb_ddr4_1tb_ssd_amd_radeon_vega_8_iluminare_rgb_5aa76ce87e4c16367530ff2aa7414470,
                    Categorie = categorieRandom
                };

                toateProdusele.Add(produs);
                ratinguri[produs] = rand.Next(1, 6); // Rating aleatoriu între 1 și 5
            }
            var produseNoi = new List<Produs>
            {
                  new Produs
                  {
                     Nume = "Imprimantă HP LaserJet M110we",
                       Pret = 499,
                         Imagine = ,
                          Categorie = "Imprimante"
                   },
                     new Produs
                     {
                         Nume = "Mouse Logitech G502",
                           Pret = 249,
                         Imagine =Properties.Resources.,
                             Categorie = "Mouse"
                     },
                       new Produs
                      {
                                Nume = "Tastatură Redragon K552",
                                Pret = 199,
                                Imagine = ,
                                 Categorie = "Tastatura"
                      }
             };
            foreach (var produs in produseNoi)
            {
                toateProdusele.Add(produs);
                ratinguri[produs] = rand.Next(1, 6);
            }
        }
        private void cmbCategorii_SelectedIndexChanged(object sender, EventArgs e)
        {
            paginaCurenta = 1;
            FiltreazaProduse();
            AfiseazaPagina(paginaCurenta);
        }

        private void FiltreazaProduse()
        {
            if (txtCautare == null || cmbCategorii == null)
            {
                MessageBox.Show("Controls are not initialized yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string cautare = txtCautare.Text.ToLower();
            string categorieSelectata = cmbCategorii.SelectedItem?.ToString() ?? "Toate";

            produseFiltrate = toateProdusele
                .Where(p =>
                    p.Nume.ToLower().Contains(cautare) &&
                    (categorieSelectata == "Toate" || p.Categorie == categorieSelectata))
                .ToList();
        }

        private void AfiseazaPagina(int pagina)
        {
            AfiseazaPagina(pagina, produseFiltrate);
        }

        private void AfiseazaPagina(int pagina, List<Produs> listaProduse)
        {
            if (flpProduse == null)
            {
                throw new InvalidOperationException("flpProduse is not initialized. Ensure InitializeLayout() is called before using this method.");
            }
            flpProduse.Controls.Clear();
            int start = (pagina - 1) * produsePePagina;
            int end = Math.Min(start + produsePePagina, listaProduse.Count);

            for (int i = start; i < end; i++)
            {
                var produs = listaProduse[i];
                var panel = CreeazaCardProdus(produs);
                flpProduse.Controls.Add(panel);
            }

            GenereazaButoanePaginare(listaProduse);
            int totalPagini = (int)Math.Ceiling(listaProduse.Count / (double)produsePePagina);
            lblPagina.Text = $"Pagina {pagina} din {totalPagini}";
        }

        private Panel CreeazaCardProdus(Produs produs)
        {
            Panel p = new Panel
            {
                Width = 160,
                Height = 220,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            PictureBox pic = new PictureBox
            {
                Image = produs.Imagine,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 140,
                Height = 100,
                Top = 5,
                Left = 10
            };

            Label lblNume = new Label
            {
                Text = produs.Nume,
                Top = 110,
                Width = 140,
                Left = 10
            };

            Label lblPret = new Label
            {
                Text = $"Pret: {produs.Pret} RON",
                Top = 130,
                Width = 140,
                Left = 10
            };

            Button btn = new Button
            {
                Text = "Adaugă în coș",
                Top = 160,
                Width = 140,
                Left = 10
            };

            btn.Click += (s, e) =>
            {
                if (!AppState.EsteLogat)
                {
                    MessageBox.Show("Trebuie să fii logat pentru a adăuga produse în coș.", "Autentificare necesară", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logare logareForm = new Logare(mainForm);
                    logareForm.ShowDialog();
                    this.Hide();
                    return;
                }
                lstCos.Items.Add($"{produs.Nume} - {produs.Pret} RON");

                if (!cosCumparaturi.Contains(produs))
                    cosCumparaturi.Add(produs);

                if (cartForm == null || cartForm.IsDisposed)
                {
                    cartForm = new CartForm(mainForm, this);
                    cartForm.Show();
                }
                else if (!cartForm.Visible)
                {
                    cartForm.Show();
                }

                cartForm.BringToFront();
                cartForm.AdaugaProdusInListBox(produs);
            };

            Label lblRating = new Label
            {
                Text = "Rating:",
                Top = 190,
                Left = 10,
                Width = 60
            };

            ComboBox cmbRating = new ComboBox
            {
                Top = 190,
                Left = 70,
                Width = 80,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbRating.Items.AddRange(new object[] { "1 ⭐", "2 ⭐⭐", "3 ⭐⭐⭐", "4 ⭐⭐⭐⭐", "5 ⭐⭐⭐⭐⭐" });

            int ratingInitial = ratinguri.ContainsKey(produs) ? ratinguri[produs] : 5;
            cmbRating.SelectedIndex = ratingInitial - 1;

            cmbRating.SelectedIndexChanged += (s, e) =>
            {
                if (!cosCumparaturi.Contains(produs))
                {
                    MessageBox.Show("Poți modifica ratingul doar după ce adaugi produsul în coș.");
                    cmbRating.SelectedIndex = ratinguri[produs] - 1;
                    return;
                }

                int ratingAles = cmbRating.SelectedIndex + 1;
                ratinguri[produs] = ratingAles;
            };

            p.Controls.Add(pic);
            p.Controls.Add(lblNume);
            p.Controls.Add(lblPret);
            p.Controls.Add(btn);
            p.Controls.Add(lblRating);
            p.Controls.Add(cmbRating);

            AdaugaClickLaToate(p, (s, e) =>
            {
                EcranInfo infoForm = new EcranInfo(produs);
                infoForm.ShowDialog();
            });

            return p;
        }


        private void AdaugaClickLaToate(Control control, EventHandler handler)
        {
            if (control is ComboBox || control is Button)
                return;
            control.Click += handler;
            foreach (Control child in control.Controls)
            {
                AdaugaClickLaToate(child, handler);
            }
        }



        private void GenereazaButoanePaginare(List<Produs> listaProduse)
        {
            flpPaginare.Controls.Clear();
            int totalPagini = (int)Math.Ceiling(listaProduse.Count / (double)produsePePagina);

            for (int i = 1; i <= totalPagini; i++)
            {
                Button btnPagina = new Button
                {
                    Text = i.ToString(),
                    Width = 40,
                    Height = 30,
                    Tag = i
                };

                btnPagina.Click += (s, e) =>
                {
                    paginaCurenta = (int)((Button)s).Tag;
                    AfiseazaPagina(paginaCurenta, listaProduse);
                };

                flpPaginare.Controls.Add(btnPagina);
            }
        }
        public void btnVeziCos_Click(object sender, EventArgs e)
        {

            CheckoutForm cf = new CheckoutForm(cosCumparaturi);

            cf.ShowDialog();

            /*CartForm cartForm = new CartForm(this, mainForm);

            cartForm.Show();*/

            //this.Hide();

        }


        private void Account_Click(object sender, EventArgs e)
        {
            AccountForm accForm = new AccountForm(mainForm, this, this.Instance);
            accForm.ShowDialog();
            this.Hide();
        }

        private void SiglaMainForm_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }
        public void AdaugaProdus(Produs produs)
        {
            if (!toateProdusele.Contains(produs))
            {
                toateProdusele.Add(produs); // Adăugăm produsul la lista globală
            }
            // Adăugăm produsul la coșul de cumpărături

            // Reset filters to ensure the new product is visible
            cmbCategorii.SelectedItem = "Toate"; // Reset category filter to "Toate"
            txtCautare.Text = ""; // Clear the search text

            // Refresh the filtered list and UI
            FiltreazaProduse();
            AfiseazaPagina(1);

            // Provide feedback to the user
            MessageBox.Show($"Produsul '{produs.Nume}' a fost adăugat cu succes!", "Produs Adăugat", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (cartForm == null)
            {
                cartForm = new CartForm(this.mainForm, this);
            }

            cartForm.SetProduse(AppState.GetProduse());

            cartForm.Show();
            cartForm.BringToFront();
            this.Hide();


        }
        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }

        public List<Produs> GetProduse()
        {
            return lstCos.Items
                .Cast<string>()
                .Select(item => new Produs
                {
                    Nume = item.Split('-')[0].Trim(),
                    Pret = decimal.Parse(item.Split('-')[1].Trim().Replace("RON", ""))
                })
                .ToList();
        }

        private void Filtre_SelectedIndexChanged(object sender, EventArgs e)
        {
            var produseCuRating = ratinguri
        .Where(kvp => kvp.Value > 0)
        .Select(kvp => kvp.Key)
        .ToList();

            if (produseCuRating.Count == 0)
            {
                MessageBox.Show("Nu ai evaluat niciun produs încă.", "Info");
                return;
            }

            paginaCurenta = 1;
            AfiseazaPagina(paginaCurenta, produseCuRating);
        }
        public void ReincarcaProduse()
        {
            var produseNoi = AppState.GetProduse(); // Obține produsele noi

            // Adăugăm produsele noi dacă nu sunt deja în lista globală
            foreach (var produs in produseNoi)
            {
                if (!toateProdusele.Any(p => p.Nume == produs.Nume && p.Pret == produs.Pret))
                {
                    toateProdusele.Add(produs);
                }
            }

            // Resetăm filtrele și afișăm produsele din nou
            cmbCategorii.SelectedItem = "Toate";
            txtCautare.Text = "";

            FiltreazaProduse(); // Filtrăm din nou lista de produse

        }
        public List<Produs> GetProduseSelectate()
        {
            // Înlocuiește cu logica reală de selectare dacă ai UI
            return AppState.GetProduse(); // sau lista locală de produse
        }
    }
}