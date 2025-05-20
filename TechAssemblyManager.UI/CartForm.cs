using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class CartForm : Form
    {
        private Dictionary<string, SelectedProduct> cartProducts = new();
        private ProductViewerForm productViewerForm;
        private MainForm mainForm;
        private MainForm mainForm1;
        private MainForm mainForm2;
        private OnorareComenziForm onorareComenziForm;
        private ProductViewerForm prvf;
        private ProductManagerBLL productManagerBLL;
        private UserManagerBLL userManagerBLL;
        private CartManagerBLL cartManagerBLL;
        private OrderManagerBLL orderManagerBLL;
        private string currentUserName;
        public CartForm(MainForm mainForm, CartManagerBLL cartManagerBLL, UserManagerBLL userManagerBLL, ProductManagerBLL productManagerBLL, OrderManagerBLL orderManagerBLL)
        {
            InitializeComponent();
            this.cartManagerBLL = cartManagerBLL;
            this.productManagerBLL = productManagerBLL;
            this.userManagerBLL = userManagerBLL;
            this.orderManagerBLL = orderManagerBLL;
            this.mainForm = mainForm;
            this.currentUserName = SessionManager.LoggedInUser.userName;
            this.FormClosing += CartForm_FormClosing;
            this.Load += CartForm_Load;
            this.Resize += CartForm_Load;
        }

        // public CartForm(MainForm mainForm, ProductManagerBLL productManagerBLL, UserManagerBLL userManagerBLL, ProductViewerForm productViewerForm = null)
        // {
        //     InitializeComponent();
        //     this.productViewerForm = productViewerForm;
        //     this.mainForm = mainForm;
        //     this.FormClosing += CartForm_FormClosing;
        //     this.Load += CartForm_Load;
        //     this.Resize += CartForm_Load;
        //     this.productManagerBLL = productManagerBLL;
        //     this.userManagerBLL = userManagerBLL;
        // }

        // public CartForm(AccountForm accountForm)
        // {
        //     AccountForm = accountForm;
        // }

        // public CartForm(OnorareComenziForm onorareComenziForm, ProductViewerForm prvf)
        // {
        //     this.onorareComenziForm = onorareComenziForm;
        //     this.prvf = prvf;
        // }

        private void CartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        // public object Instance { get; private set; }
        // public object CosCumparaturi { get; internal set; }
        // public object getInstance { get; internal set; }
        // public AccountForm AccountForm { get; }

        private async void CartForm_Load(object sender, EventArgs e)
        {
            await LoadCartAsync();
            StyleControls();
        }


        // Metodă ca să adaugi un produs în ListBox și să actualizezi totalul
        // public async Task AdaugaProdusInListBox(Product produs)
        // {
        //     // await AddProductToCartAsync(produs.productId);
        //     await 
        //     listBox1.Items.Add($"{produs.name} - {produs.price} RON");
        //     AppState.AdaugaProdus(produs);
        //     CalculeazaTotal();
        // }
        // Add the missing GetProduse method to the ProductViewerForm class
        // public async Task AddProductToCartAsync(string productId, int quantity)
        // {
        //     await cartManagerBLL.AddProductToCartAsync(currentUserName, productId, quantity);
        //     await LoadCartAsync();
        // }

        // Example method to remove a product from cart
        public async Task RemoveProductFromCartAsync(string productId)
        {
            await cartManagerBLL.RemoveProductFromCartAsync(currentUserName, productId);
            await LoadCartAsync();
        }

        private async Task LoadCartAsync()
        {
            listBox1.Items.Clear();
            cartProducts = await cartManagerBLL.GetUserCartAsync(currentUserName);

            decimal total = 0;
            foreach (var entry in cartProducts)
            {
                var product = await productManagerBLL.GetProductByIdAsync(entry.Key);
                if (product != null)
                {
                    decimal price = (decimal)product.price * entry.Value.quantity;
                    listBox1.Items.Add($"{product.name} x{entry.Value.quantity} - {price} RON");
                    total += price;
                }
            }
            Total.Text = $"Total: {total} RON";
        }



        private void backbutton_Click(object sender, EventArgs e)
        {
            productViewerForm.Show();
            productViewerForm.Text = "ProductViewerForm";
            this.Hide();
        }

        private void PlasareComanda_Click(object sender, EventArgs e)
        {
            OrderInformation orderInfo = new OrderInformation(mainForm, productManagerBLL, userManagerBLL, orderManagerBLL, SessionManager.LoggedInUser);
            orderInfo.Show();
            this.Hide();
        }
        // public void SetProduse(List<Product> produse)
        // {
        //     produseAdaugate = produse;
        //     listBox1.Items.Clear();
        //     foreach (var produs in produse)
        //     {
        //         listBox1.Items.Add($"{produs.name} - {produs.price} RON");
        //     }
        //     CalculeazaTotal();
        // }
        private void Myaccount_Click(object sender, EventArgs e)
        {
            AccountForm accForm = new AccountForm(mainForm, productManagerBLL, userManagerBLL, orderManagerBLL);
            accForm.ShowDialog();
            this.Hide();
        }
        private void StyleControls()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int margin = 10;

            this.BackColor = Color.White;
            Font modernFont = new Font("Segoe UI", 10, FontStyle.Bold);
            backbutton.Font = modernFont;
            Cosdecumparaturi.Font = modernFont;
            Myaccount.Font = modernFont;
            PlasareComanda.Font = modernFont;
            Total.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            listBox1.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            Color accentColor = Color.FromArgb(0, 120, 215);

            Button[] buttons = { backbutton, Myaccount, PlasareComanda };
            foreach (Button btn in buttons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = accentColor;
                btn.ForeColor = Color.White;
                btn.Height = 35;
                btn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 15, 15));
            }

            Total.BorderStyle = BorderStyle.FixedSingle;
            Total.BackColor = Color.WhiteSmoke;
            Total.ForeColor = Color.Black;

            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.BackColor = Color.WhiteSmoke;

            backbutton.Left = margin;
            backbutton.Top = margin;
            Myaccount.Top = margin;
            Myaccount.Left = formWidth - Myaccount.Width - margin;

            int verticalCenter = formHeight / 2;
            int listBoxTop = verticalCenter - listBox1.Height - Total.Height - PlasareComanda.Height;

            listBox1.Top = listBoxTop;
            listBox1.Left = (formWidth - listBox1.Width) / 2;

            Total.Top = listBox1.Bottom + margin;
            Total.Left = (formWidth - Total.Width) / 2;

            PlasareComanda.Top = Total.Bottom + margin;
            PlasareComanda.Left = (formWidth - PlasareComanda.Width) / 2;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

    }
}