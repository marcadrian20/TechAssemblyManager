using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class AccountForm : Form
    {
        private Form f;
        private ListBox b;
        private CartForm cartForm;
        private MainForm _mainForm;
        private ProductViewerForm _productViewerForm;
        public Label lblPassword;
        private Button btnAutentificare;
        private Label lblComenzi;
        private MainForm.User utilizator = new MainForm.User("Guest", "guest@email.com");

        public MainForm Instance { get; }

        // Constructor cu parametrul Form f
        public AccountForm(MainForm mainForm, Form f)
        {
            InitializeComponent();
            this.f = f;
            _mainForm = mainForm;

            // Inițializează lblPassword și adaugă-l la formular
            lblPassword = new Label();
            lblPassword.Name = "lblPassword";
            lblPassword.Text = "Parola: *****"; // Text implicit
            lblPassword.AutoSize = true;
            this.Controls.Add(lblPassword);
            btnAutentificare = new Button
            {
                Text = "Autentificare",
                Width = 150
            };
            btnAutentificare.Click += BtnAutentificare_Click;
            this.Controls.Add(btnAutentificare);

            // Label pentru comenzile utilizatorului
            lblComenzi = new Label
            {
                Text = "Comenzile tale vor apărea aici după autentificare.",
                AutoSize = true,
                MaximumSize = new Size(300, 0)
            };
            this.Controls.Add(lblComenzi);
            b = new ListBox
            {
                Width = 300,
                Height = 100
            };
            this.Controls.Add(b);
            // Centrează controalele
            CenterControls();

            // Atașăm un eveniment de redimensionare pentru formular
            this.Resize += AccountForm_Resize;
        }

        public AccountForm(MainForm instance)
        {
            Instance = instance;
        }
        public AccountForm(CartForm cartForm)
        {
            this.cartForm = cartForm;
        }
        public AccountForm(ProductViewerForm productViewerForm)
        {
            _productViewerForm = productViewerForm;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delogat cu succes!");
            f.Hide();
            _mainForm.Show();
            this.Hide();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Utilizator: Guest";

            // Atașăm un eveniment de click pe label pentru a seta parola
            lblPassword.Click += new EventHandler(lblPassword_Click);

            // Centrează controalele
            CenterControls();
        }
        private void BtnAutentificare_Click(object sender, EventArgs e)
        {
            string parola = lblPassword.Text.StartsWith("Parola: ") ? lblPassword.Text.Substring(8).Trim() : "";
            if (string.IsNullOrEmpty(parola) || parola == "*****")
            {
                MessageBox.Show("Introduceți parola înainte de autentificare!");
                return;
            }
            OrderData orderData = new OrderData();
            if (utilizator.Autentifica(parola))
            {
                MessageBox.Show("Autentificare reușită!");
                if (utilizator.DateLivrare == null)
                {
                    var orderInfo = new OrderInformation(_mainForm);
                    orderInfo.EsteAutentificat = true;
                    orderInfo.ShowDialog();

                    // 🔒 Verifică dacă a apăsat butonul "Trimitere"
                    if (!orderInfo.ClickATrimis)
                    {
                        MessageBox.Show("Comanda nu a fost trimisă.");
                        return;
                    }

                    orderData = orderInfo.GetOrderData();
                    if (string.IsNullOrWhiteSpace(orderData.Nume) ||
                        string.IsNullOrWhiteSpace(orderData.Adresa) ||
                        string.IsNullOrWhiteSpace(orderData.Telefon) ||
                        string.IsNullOrWhiteSpace(orderData.Email))
                    {
                        MessageBox.Show("Comanda nu a fost completată.");
                        return;
                    }
                    utilizator.DateLivrare = orderData;
                }
                if (cartForm == null)
                    cartForm = new CartForm(_mainForm);
                cartForm.SetProduse(AppState.GetProduse());
                List<Produs> produse = cartForm.GetProduse();

                if (produse.Count == 0)
                {
                    MessageBox.Show("Nu există produse selectate pentru comandă.");
                    return;
                }

                var comanda = new Comanda(produse, orderData.Nume, orderData.Adresa, orderData.Telefon, orderData.Email);
                utilizator.Comenzi.Add(comanda);

                b.Items.Add($"Comanda din {comanda.DataComenzii.ToShortDateString()} - {comanda.Produse.Count} produse");
                foreach (var produs in comanda.Produse)
                {
                    b.Items.Add($"Produs: {produs.Nume}, Preț: {produs.Pret} RON");
                }

                lblComenzi.Text = "Comanda a fost plasată cu succes!";
                CenterControls();
            }
        }
        private void lblPassword_Click(object sender, EventArgs e)
        {
            // Creați un formular pentru a cere parola (utilizăm un InputBox)
            string password = Microsoft.VisualBasic.Interaction.InputBox("Introduceți parola:", "Setare Parolă", "", -1, -1);

            // Dacă utilizatorul nu a lăsat câmpul gol, actualizăm textul etichetei
            if (!string.IsNullOrEmpty(password))
            {
                SetPassword(password);
            }
            else
            {
                MessageBox.Show("Nu ați introdus nicio parolă!");
            }
        }

        public void SetPassword(string password)
        {
            if (lblPassword != null)
            {
                lblPassword.Text = $"Parola: {password}";
            }
        }

        // Funcție pentru a centra toate controalele
        private void CenterControls()
        {
            // Obține dimensiunile formularului
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Calculăm lățimea totală și înălțimea totală a tuturor controalelor
            int totalWidth = 0;
            int totalHeight = 0;
            int verticalSpacing = 10; // Spațiu între controale

            // Calculăm înălțimea totală a tuturor controalelor
            foreach (Control control in this.Controls)
            {
                totalHeight += control.Height;
                totalWidth = Math.Max(totalWidth, control.Width); // Determinăm lățimea maximă
            }

            // Calculează spațiu total între controale
            totalHeight += (this.Controls.Count - 1) * verticalSpacing;

            // Calculăm poziția de start pe axa Y și X
            int startingX = (formWidth - totalWidth) / 2; // Poziția pe axa X (centrat)
            int startingY = (formHeight - totalHeight) / 2; // Poziția pe axa Y (centrat)

            // Poziționează fiecare control pe formular
            int currentY = startingY;
            foreach (Control control in this.Controls)
            {
                // Poziționează controlul pe axa X
                int controlX = (formWidth - control.Width) / 2; // Centrăm pe axa X

                // Setează poziția controlului
                control.Location = new Point(controlX, currentY);

                // Actualizează Y pentru următorul control
                currentY += control.Height + verticalSpacing;
            }
        }

        private void AccountForm_Resize(object sender, EventArgs e)
        {
            // Centrează din nou controalele la redimensionarea formularului
            CenterControls();
        }

        private void back_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
