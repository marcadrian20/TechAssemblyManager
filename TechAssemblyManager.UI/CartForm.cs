using System;
using System.Drawing;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class CartForm : Form
    {
        private List<Produs> produseAdaugate = new List<Produs>();
        private ProductViewerForm productViewerForm;
        private MainForm mainForm;
        private MainForm mainForm1;
        private MainForm mainForm2;
        private OnorareComenziForm onorareComenziForm;
        private ProductViewerForm prvf;

        public CartForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.Load += CartForm_Load;
            this.Resize += CartForm_Load;
        }

        public CartForm(MainForm mainForm, ProductViewerForm productViewerForm = null)
        {
            InitializeComponent();
            this.productViewerForm = productViewerForm;
            this.mainForm = mainForm;
            this.FormClosing += CartForm_FormClosing;
            this.Load += CartForm_Load;
            this.Resize += CartForm_Load;
        }

        public CartForm(AccountForm accountForm)
        {
            AccountForm = accountForm;
        }

        public CartForm(OnorareComenziForm onorareComenziForm, ProductViewerForm prvf)
        {
            this.onorareComenziForm = onorareComenziForm;
            this.prvf = prvf;
        }

        private void CartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        public object Instance { get; private set; }
        public object CosCumparaturi { get; internal set; }
        public object getInstance { get; internal set; }
        public AccountForm AccountForm { get; }

        private void CartForm_Load(object sender, EventArgs e)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int margin = 10;

            // Setări generale
            this.BackColor = Color.White;

            Font modernFont = new Font("Segoe UI", 10, FontStyle.Bold);
            backbutton.Font = modernFont;
            Cosdecumparaturi.Font = modernFont;
            Myaccount.Font = modernFont;
            PlasareComanda.Font = modernFont;
            Total.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            listBox1.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // Margini rotunjite și culori
            Color accentColor = Color.FromArgb(0, 120, 215); // Albastru Windows

            Button[] buttons = { backbutton,  Myaccount, PlasareComanda };
            foreach (Button btn in buttons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = accentColor;
                btn.ForeColor = Color.White;
                btn.Height = 35;
                btn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 15, 15));
            }

            // TextBox stilizat
            Total.BorderStyle = BorderStyle.FixedSingle;
            Total.BackColor = Color.WhiteSmoke;
            Total.ForeColor = Color.Black;

            // ListBox stilizat
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.BackColor = Color.WhiteSmoke;

            // Poziționare
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
        // Metodă ca să adaugi un produs în ListBox și să actualizezi totalul
        public void AdaugaProdusInListBox(Produs produs)
        {
            produseAdaugate.Add(produs);
            listBox1.Items.Add($"{produs.Nume} - {produs.Pret} RON");
            AppState.AdaugaProdus(produs);
            CalculeazaTotal();
        }
        // Add the missing GetProduse method to the ProductViewerForm class
        public List<Produs> GetProduse()
        {
            return new List<Produs>(produseAdaugate);
        }

        private void CalculeazaTotal()
        {
            decimal total = 0;
            foreach (var item in listBox1.Items)
            {
                string text = item.ToString();
                int index = text.LastIndexOf('-');
                if (index >= 0)
                {
                    string pretText = text.Substring(index + 1).Replace("RON", "").Trim();
                    if (decimal.TryParse(pretText, out decimal pret))
                    {
                        total += pret;
                    }
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
            OrderInformation orderInfo = new OrderInformation(mainForm);
            orderInfo.Show();
            this.Hide();
        }
        public void SetProduse(List<Produs> produse)
        {
            produseAdaugate = produse;
            listBox1.Items.Clear();
            foreach (var produs in produse)
            {
                listBox1.Items.Add($"{produs.Nume} - {produs.Pret} RON");
            }
            CalculeazaTotal();
        }
        private void Myaccount_Click(object sender, EventArgs e)
        {
            AccountForm accForm = new AccountForm(mainForm, this,productViewerForm);
            accForm.ShowDialog();
            this.Hide();
        }
    }
}
