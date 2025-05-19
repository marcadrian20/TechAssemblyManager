using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FirebaseWrapper;
using TechAssemblyManager.Models;
using TechAssemblyManager.BLL;

namespace TechAssemblyManager.UI
{
    public partial class MainForm : Form
    {
        /// 
        /// /REFACTOR
        /// 
        PromotiiForm promotiiForm;
        public CartForm cartForm;
        ProductViewerForm prvf;
        MainForm mainForm;
        private Label lblComenzi;
        private ListBox lstComenzi;
        private Button Login;
        private Button btnIstoricService;
        Form f;
        private List<Product> produseInCos = new List<Product>();

        //END REFACTOR
        //HELPER&BLL
        private User currentUser;
        private FirebaseHelper firebaseHelper;
        private ProductManagerBLL productManagerBLL;
        private UserManagerBLL userManagerBLL;
        private OrderManagerBLL orderManagerBLL;
        private PromotionManagerBLL promotionManagerBLL;

        /// ////
        public MainForm()
        {
            InitializeComponent();
            firebaseHelper = new FirebaseHelper(
                "https://techassemblymanager-default-rtdb.firebaseio.com/",
                "ky7wJX7Iu46hjBHWqDJNWjJW19NeYQurX4Z9VeUv",
                "AIzaSyBxq3J01JqE6yonLc9plkzA6c3-Gi1r1eU"
            );
            productManagerBLL = new ProductManagerBLL(firebaseHelper);
            userManagerBLL = new UserManagerBLL(firebaseHelper);
            orderManagerBLL = new OrderManagerBLL(); // Implement as needed
            promotionManagerBLL = new PromotionManagerBLL(); // Implement as needed


            ////ANALYZE DOWN BELOW
            this.KeyPreview = true;
            Instance = this;
            prvf = new ProductViewerForm(this, null, productManagerBLL, null);
            if (Login == null)
            {
                Login = new Button()
                {
                    Text = "Login",
                    Size = new Size(75, 23)
                };
                Login.Location = new Point
                    (
                        cos.Left,
                        cos.Bottom + 10
                    );
                Login.Click += Login_Click;
            }
            pictureBox1.Image = (Image)Properties.Resources.pcgarage.Clone();
            TechAssemblyManager.Controls.Add(Login);
            btnIstoricService = new Button()
            {
                Text = "Istoric Service",
                Size = new Size(120, 30)
            };
            btnIstoricService.Location = new Point
                (
                    Login.Left,
                    Login.Bottom + 10
                );
            btnIstoricService.Click += BtnIstoricService_Click;
            TechAssemblyManager.Controls.Add(btnIstoricService);
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int centerX = (screen.Width - TechAssemblyManager.Width) / 2;
            int centerY = (screen.Height - TechAssemblyManager.Height) / 2;
            TechAssemblyManager.Location = new Point(centerX, centerY);
            Instance = this;

        }
        public MainForm(MainForm instance = null)
        {
            Instance = instance ?? this; // Ensure Instance is never null
            InitializeComponent();
        }

        //DEBUG
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                var debugForm = new DebugWindowForm();
                debugForm.Show();
            }
        }
        //END DEBUG
        private void ViewCatalog_Click(object sender, EventArgs e)
        {
            CatalogProduse c = new CatalogProduse(this.Instance);
            c.Show();
            this.Hide();
        }

        private void Promotii_Click(object sender, EventArgs e)
        {
            promotiiForm = new PromotiiForm(this.Instance);
            promotiiForm.Show();
            this.Hide();
        }
        private void BtnIstoricService_Click(object sender, EventArgs e)
        {
            VizualizareCereriForm vizualizareCereriForm = new VizualizareCereriForm(AppState.UtilizatorCurent, this); // Transmite instanța curentă
            vizualizareCereriForm.Show();
            this.Hide();
        }
        private void Myaccount_Click(object sender, EventArgs e)
        {
            var productViewerForm = new ProductViewerForm(this, cartForm, productManagerBLL, null); // Pass the current MainForm and CartForm
            var accountForm = new AccountForm(this, this, productViewerForm, productManagerBLL); // Pass MainForm, current form, and ProductViewerForm
            accountForm.Show();
            this.Hide(); //
        }

        public void AddProdusToCos(Produs produs)
        {
            AppState.AdaugaProdus(produs);
        }
        private void AfiseazaComenzi(User user)
        {
            lstComenzi.Items.Clear();

            foreach (var comanda in user.Comenzi)
            {
                lstComenzi.Items.Add($"Comanda din {comanda.DataComenzii.ToShortDateString()} - {comanda.Produse.Count} produse");
            }
        }
        private void cos_Click(object sender, EventArgs e)
        {
            if (cartForm == null)
            {
                cartForm = new CartForm(this, productManagerBLL, prvf);
            }

            cartForm.SetProduse(AppState.GetProduse());

            cartForm.Show();
            cartForm.BringToFront();
            this.Hide();
        }

        public void AcceseazaProduseDinCos()
        {
            if (cartForm != null)
            {
                List<Produs> produse = cartForm.GetProduse();
                foreach (var produs in produse)
                {
                    Console.WriteLine($"Produs: {produs.Nume}, Preț: {produs.Pret}");
                }
            }
        }

        private void Searchbar_TextChanged(object sender, EventArgs e) { }
        private void TechAssemblyManager_Enter(object sender, EventArgs e) { }
        private void CerereService_Click(object sender, EventArgs e)
        {
            CerereServiceForm cerereForm = new CerereServiceForm(this.Instance);
            cerereForm.ShowDialog();
        }

        public MainForm Instance { get; }
        // public class User
        // {
        //     public string Nume { get; set; }
        //     public string Email { get; set; }
        //     public List<Comanda> Comenzi { get; set; }
        //     public bool EsteAutentificat { get; set; }
        //     public OrderData DateLivrare { get; set; }

        //     private string parola;

        //     public User(string nume, string email)
        //     {
        //         Nume = nume;
        //         Email = email;
        //         Comenzi = new List<Comanda>();
        //         parola = "admin123";
        //         EsteAutentificat = false;

        //     }

        //     public bool Autentifica(string parolaIntrodusa)
        //     {
        //         if (parolaIntrodusa == parola)
        //         {
        //             EsteAutentificat = true;
        //             return true;
        //         }
        //         return false;
        //     }
        // }

        private async void Login_Click(object sender, EventArgs e)
        {
            Logare logare = new Logare(this.Instance);
            logare.Show();
            this.Hide();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.KeyDown += MainForm_KeyDown;

            Login = new Button()
            {
                Text = "Login",
                Location = new Point(10, 10),
                Size = new Size(75, 23)
            };
            Login.Click += Login_Click;
        }
    }
}