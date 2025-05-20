using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class ProductViewerForm : Form
    {
        private MainForm mainForm;
        private ProductManagerBLL productManagerBLL;
        private CartManagerBLL cartManagerBLL;
        private UserManagerBLL userManagerBLL;
        private OrderManagerBLL orderManagerBLL;

        private List<Product> toateProdusele = new List<Product>();
        private List<Product> produseFiltrate = new List<Product>();
        private List<Product> cosCumparaturi = new List<Product>();
        private Dictionary<Product, int> ratinguri = new Dictionary<Product, int>();
        private int produsePePagina = 10;
        private int paginaCurenta = 1;
        private string categoryID;
        private FlowLayoutPanel flpProduse;
        private FlowLayoutPanel flpPaginare;
        private TextBox txtCautare;
        private Button btnCauta;
        private ListBox lstCos;
        private Label lblCos;
        private ComboBox cmbCategorii;
        private Button btnAccount;
        // private object instance;
        /// <summary>
        /// 
        /// </summary>
        // public ProductViewerForm Instance { get; set; }

        public ProductViewerForm(MainForm mainForm, ProductManagerBLL productManagerBLL, CartManagerBLL cartManagerBLL, UserManagerBLL userManagerBLL, OrderManagerBLL orderManagerBLL, string categoryID)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.productManagerBLL = productManagerBLL;
            this.cartManagerBLL = cartManagerBLL;
            this.userManagerBLL = userManagerBLL;
            this.orderManagerBLL = orderManagerBLL;
            // this.cartForm = cartForm;
            // Instance = this;
            this.categoryID = categoryID;
            this.Text = "Catalog Produse";
            this.Size = new Size(1000, 700);
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Font = new Font("Segoe UI", 10);
            InitializeLayout();
            LoadProduseAsync();

            cmbCategorii.SelectedIndexChanged -= cmbCategorii_SelectedIndexChanged;
            cmbCategorii.Items.Clear();
            string[] categorii = { "Toate", "Laptopuri", "Desktopuri", "Monitoare" };
            cmbCategorii.Items.AddRange(categorii);

            if (!string.IsNullOrEmpty(categoryID) && cmbCategorii.Items.Contains(categoryID))
                cmbCategorii.SelectedItem = categoryID;
            else
                cmbCategorii.SelectedIndex = 0;

            // #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            cmbCategorii.SelectedIndexChanged += cmbCategorii_SelectedIndexChanged;
            // #pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            this.FormClosing += ProductViewerForm_FormClosing;
            this.Load += ProductViewerForm_Load;

            // FiltreazaProduse();
            // AfiseazaPagina(1);
        }
        private void ProductViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }

        private async void ProductViewerForm_Load(object sender, EventArgs e)
        {
            await LoadProduseAsync();
            FiltreazaProduse();
            AfiseazaPagina(1);
        }
        public ProductViewerForm getInstance()
        {
            return this;
        }

        private void InitializeLayout()
        {
            int offsetDreapta = 280;

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
                Location = new Point(500, 10),
                Width = 120,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnReincarca.Click += async (s, e) =>
            {
                await LoadProduseAsync();
                FiltreazaProduse();
                AfiseazaPagina(1);
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

            btnAccount = new Button
            {
                Text = "Contul meu",
                Location = new Point(730 + offsetDreapta, 560),
                Size = new Size(240, 30)
            };
            btnAccount.Click += Account_Click;
            this.Controls.Add(btnAccount);
        }

        private async Task LoadProduseAsync()
        {
            toateProdusele = await productManagerBLL.GetProductsByCategoryAsync(categoryID);
        }
        private void cmbCategorii_SelectedIndexChanged(object sender, EventArgs e)
        {
            paginaCurenta = 1;
            FiltreazaProduse();
            AfiseazaPagina(paginaCurenta);
        }

        private void FiltreazaProduse()
        {
            // if (txtCautare == null || cmbCategorii == null)
            // {
            //     MessageBox.Show("Controls are not initialized yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //     return;
            // }
            string cautare = txtCautare.Text.ToLower();
            string categorieSelectata = cmbCategorii.SelectedItem?.ToString() ?? "Toate";

            produseFiltrate = toateProdusele
                .Where(p =>
                    p.name.ToLower().Contains(cautare) &&
                    (categorieSelectata == "Toate" || p.categoryId == categorieSelectata))
                .ToList();
        }

        private void AfiseazaPagina(int pagina)
        {
            AfiseazaPagina(pagina, produseFiltrate);
        }

        private void AfiseazaPagina(int pagina, List<Product> listaProduse)
        {
            // if (flpProduse == null)
            // {
            //     throw new InvalidOperationException("flpProduse is not initialized. Ensure InitializeLayout() is called before using this method.");
            // }
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

        private Panel CreeazaCardProdus(Product produs)
        {
            Panel p = new Panel
            {
                Width = 160,
                Height = 220,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            // PictureBox pic = new PictureBox
            // {
            //     Image = produs.Imagine,
            //     SizeMode = PictureBoxSizeMode.Zoom,
            //     Width = 140,
            //     Height = 100,
            //     Top = 5,
            //     Left = 10
            // };

            Label lblname = new Label
            {
                Text = produs.name,
                Top = 110,
                Width = 140,
                Left = 10
            };

            Label lblprice = new Label
            {
                Text = $"price: {produs.price} RON",
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

            btn.Click += async (s, e) =>
            {
                if (SessionManager.LoggedInUser == null)
                {
                    MessageBox.Show("Trebuie să fii logat pentru a adăuga produse în coș.", "Autentificare necesară", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logare logareForm = new Logare(mainForm,userManagerBLL);
                    logareForm.ShowDialog();
                    this.Hide();
                    return;
                }
                lstCos.Items.Add($"{produs.name} - {produs.price} RON");

                await cartManagerBLL.AddProductToCartAsync(SessionManager.LoggedInUser.userName, produs.productId, 1);
                MessageBox.Show($"{produs.name} a fost adăugat în coș!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            // if (cartForm == null || cartForm.IsDisposed)
            // {
            //     cartForm = new CartForm(mainForm, productManagerBLL, userManagerBLL, this);
            //     cartForm.Show();
            // }
            // else if (!cartForm.Visible)
            // {
            //     cartForm.Show();
            // }

            // cartForm.BringToFront();
            // cartForm.AdaugaProdusInListBox(produs);


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

            //p.Controls.Add(pic);
            p.Controls.Add(lblname);
            p.Controls.Add(lblprice);
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



        private void GenereazaButoanePaginare(List<Product> listaProduse)
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

            var cartForm = new CartForm(mainForm, cartManagerBLL, userManagerBLL, productManagerBLL, orderManagerBLL);
            cartForm.Show();
            cartForm.BringToFront();
            this.Hide();

            /*CartForm cartForm = new CartForm(this, mainForm);

            cartForm.Show();*/

            //this.Hide();

        }


        private void Account_Click(object sender, EventArgs e)
        {
            AccountForm accForm = new AccountForm(mainForm, productManagerBLL, userManagerBLL, orderManagerBLL);
            accForm.ShowDialog();
            this.Hide();
        }

        private void SiglaMainForm_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }
        // public void AdaugaProdus(Produs produs)
        // {
        //     if (!toateProdusele.Contains(produs))
        //     {
        //         toateProdusele.Add(produs); // Adăugăm produsul la lista globală
        //     }
        //     // Adăugăm produsul la coșul de cumpărături

        //     // Reset filters to ensure the new product is visible
        //     cmbCategorii.SelectedItem = "Toate"; // Reset category filter to "Toate"
        //     txtCautare.Text = ""; // Clear the search text

        //     // Refresh the filtered list and UI
        //     FiltreazaProduse();
        //     AfiseazaPagina(1);

        //     // Provide feedback to the user
        //     MessageBox.Show($"Produsul '{produs.name}' a fost adăugat cu succes!", "Produs Adăugat", MessageBoxButtons.OK, MessageBoxIcon.Information);
        // }

        // private void button13_Click(object sender, EventArgs e)
        // {
        //     if (cartForm == null)
        //     {
        //         cartForm = new CartForm(this.mainForm, productManagerBLL, userManagerBLL, this);
        //     }

        //     cartForm.SetProduse(AppState.GetProduse());

        //     cartForm.Show();
        //     cartForm.BringToFront();
        //     this.Hide();


        // }
        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }

        // public List<Produs> GetProduse()
        // {
        //     return lstCos.Items
        //         .Cast<string>()
        //         .Select(item => new Produs
        //         {
        //             name = item.Split('-')[0].Trim(),
        //             price = decimal.Parse(item.Split('-')[1].Trim().Replace("RON", ""))
        //         })
        //         .ToList();
        // }

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
        // public void ReincarcaProduse()
        // {
        //     var produseNoi = AppState.GetProduse(); // Obține produsele noi

        //     // Adăugăm produsele noi dacă nu sunt deja în lista globală
        //     foreach (var produs in produseNoi)
        //     {
        //         if (!toateProdusele.Any(p => p.name == produs.name && p.price == produs.price))
        //         {
        //             toateProdusele.Add(produs);
        //         }
        //     }

        //     // Resetăm filtrele și afișăm produsele din nou
        //     cmbCategorii.SelectedItem = "Toate";
        //     txtCautare.Text = "";

        //     FiltreazaProduse(); // Filtrăm din nou lista de produse

        // }
        // public List<Produs> GetProduseSelectate()
        // {
        //     // Înlocuiește cu logica reală de selectare dacă ai UI
        //     return AppState.GetProduse(); // sau lista locală de produse
        // }
    }
}