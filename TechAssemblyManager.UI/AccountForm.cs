using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechAssemblyManager.UI
{
    public partial class AccountForm : Form
    {
        private Label lblUser;
        private Form f;
        private ListBox b;
        private CartForm cartForm;
        private MainForm _mainForm;
        private ProductViewerForm _productViewerForm;
        public Label lblPassword;
        private Button btnAutentificare;
        private Label lblComenzi;
        // Pentru testare:
        private MainForm.User utilizator;
        public MainForm Instance { get; }
        // Constructor cu parametrul Form f
        public AccountForm(MainForm mainForm, Form f, ProductViewerForm productViewerForm)
        {
            InitializeComponent();
            this.f = f;
            _productViewerForm = productViewerForm;
            _mainForm = mainForm;
            Instance = mainForm.Instance??mainForm;
            utilizator = AppState.UtilizatorCurent;
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
            lblUser = new Label
            {
                Name = "lblUser",
                AutoSize = true
            };
            this.Controls.Add(lblUser);
            // Centrează controalele
            CenterControls();
            this.FormClosing += AccountForm_FormClosing;

            // Atașăm un eveniment de redimensionare pentru formular
            this.Resize += AccountForm_Resize;
        }
        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Afișează MainForm când AccountForm este închis
            if (_mainForm != null && !_mainForm.IsDisposed)
            {
                _mainForm.Show();
            }
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
            utilizator = AppState.UtilizatorCurent;

            if (utilizator != null && utilizator.EsteAutentificat)
            {
                string tipCont = "Client";
                if (utilizator is AngajatSenior)
                {
                    tipCont = "Angajat Senior";
                }
                else if (utilizator is AngajatJunior)
                {
                    tipCont = "Angajat Junior";
                }
                else if (utilizator is Manager)
                {
                    tipCont = "Manager";
                }
                else 
                {
                    tipCont = "Angajat";
                }

                lblUser.Text = $"Utilizator: {utilizator.Nume} ({tipCont})";
            }
            else
            {
                lblUser.Text = "Utilizator: Guest";
            }
            lblPassword.Click += new EventHandler(lblPassword_Click);
            lblUser.Text = "Utilizator: Guest";

            // Atașăm un eveniment de click pe label pentru a seta parola
            lblPassword.Click += new EventHandler(lblPassword_Click);
            CenterControls();
        }
        private void BtnAutentificare_Click(object sender, EventArgs e)
        {
            string parola = GetParolaDinLabel();

            if (!ValidareParola(parola))
                return;
            if (!UserManager.Autentifica(utilizator.Email, parola))
            {
                MessageBox.Show("Parolă incorectă!");
                return;
            }
            // Obține utilizatorul corect din UserManager
            var utilizatorCorect = UserManager.GetUserByEmail(utilizator.Email);
            if (utilizatorCorect == null)
            {
                MessageBox.Show("Eroare la autentificare. Utilizatorul nu a fost găsit.");
                return;
            }
            if (AsiguraDateLivrare(utilizator, out OrderData orderData))
            {
                var comenzi = AppState.GetProduse();
                b.Items.Add($"Date livrare: {orderData.Nume}, {orderData.Adresa}, {orderData.Telefon}, {orderData.Email}");
                if (cartForm == null)
                {
                    cartForm = new CartForm(_mainForm, _productViewerForm);
                }
                cartForm.SetProduse(AppState.GetProduse());
                if (cartForm != null)
                {
                    List<Produs> produse = cartForm.GetProduse();
                    foreach (var produs in produse)
                    {
                        b.Items.Add($"Produs: {produs.Nume}, Preț: {produs.Pret}");
                    }
                }
            }
            else
            {
                MessageBox.Show("trebuie completaTE datele de livrare");
            }
            utilizatorCorect.EsteAutentificat = true;
            AppState.UtilizatorCurent = utilizatorCorect;

            AfiseazaDetaliiUtilizator(utilizatorCorect);
            AdaugaControaleSpecifice(utilizatorCorect);

            MessageBox.Show("Autentificare reușită!");
            CenterControls();
        }
        private string GetParolaDinLabel()
        {
            return lblPassword.Text.StartsWith("Parola: ") ? lblPassword.Text.Substring(8).Trim() : "";
        }

        private bool ValidareParola(string parola)
        {
            if (string.IsNullOrEmpty(parola) || parola == "*****")
            {
                MessageBox.Show("Introduceți parola înainte de autentificare!");
                return false;
            }
            return true;
        }

        private void AfiseazaDetaliiUtilizator(MainForm.User user)
        {
            string tipCont = GetTipCont(user);
            lblUser.Text = $"Utilizator: {user.Nume} ({tipCont})";
        }

        private void AdaugaControaleSpecifice(MainForm.User user)
        {
            if (_mainForm == null || Instance == null)
            {
                MessageBox.Show("MainForm or Instance is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (user is AngajatSenior)
            {
                Button btnAdaugaProdus = new Button
                {
                    Text = "Adaugă produs nou",
                    Width = 150
                };
                btnAdaugaProdus.Click += (s, ev) => {
                    var form = new AdaugaProdusForm(_productViewerForm, _mainForm);
                    form.FormClosed += (se, ea) => _mainForm.Show(); // <- revine la MainForm
                    form.Show();
                };

                this.Controls.Add(btnAdaugaProdus);
                Button btnComenzi = new Button
                {
                    Text = "Vezi comenzi de onorat",
                    Width = 180
                };
                btnComenzi.Click += (s, ev) => {
                    {
                        var form = new OnorareComenziForm(user, this.Instance);
                        form.FormClosed += (se, ea) => _mainForm.Show(); // <- revine la MainForm
                        form.Show();
                    }
                    ;
                };
                this.Controls.Add(btnComenzi);

                Button btnService = new Button
                {
                    Text = "Vezi cereri service",
                    Width = 180
                };
                btnService.Click += (s, ev) => {
                    {
                        var form = new VizualizareCereriForm(user, this.Instance);
                        form.FormClosed += (se, ea) => _mainForm.Show(); // <- revine la MainForm
                        form.Show();
                    }
                    ;
                };
                this.Controls.Add(btnService);
            }
            if (user is AngajatJunior)
            {
                Button btnComenzi = new Button
                {
                    Text = "Vezi comenzi de onorat",
                    Width = 180
                };
                btnComenzi.Click += (s, ev) => {
                    var form=new OnorareComenziForm(user, this.Instance);
                     form.FormClosed += (se, ea) => _mainForm.Show(); // <- revine la MainForm
                    form.Show();
                };
                this.Controls.Add(btnComenzi);

                Button btnService = new Button
                {
                    Text = "Vezi cereri service",
                    Width = 180
                };
                btnService.Click += (s, ev) => {
                    var form = new VizualizareCereriForm(user,_mainForm);
                    form.ShowDialog();
                };
                this.Controls.Add(btnService);
            }

            if (user is Manager)
            {
                Button btnPromo = new Button
                {
                    Text = "Gestionează promoții",
                    Width = 150
                };
                btnPromo.Click += (s, ev) => new GestioneazaPromotiiForm(_mainForm).ShowDialog();
                this.Controls.Add(btnPromo);
                Button btnGestionareAngajati = new Button
                {
                    Text = "Gestionare Angajați",
                    Width = 180
                };
                btnGestionareAngajati.Click += (s, ev) =>new ManagerForm().ShowDialog();
                this.Controls.Add(btnGestionareAngajati);
            }
        }

        public bool AsiguraDateLivrare(MainForm.User user, out OrderData orderData)
        {
            orderData = user.DateLivrare;
            if (orderData != null)
                return true;

            var orderInfo = new OrderInformation(_mainForm)
            {
                EsteAutentificat = true
            };
            orderInfo.ShowDialog();

            if (!orderInfo.ClickATrimis)
            {
                MessageBox.Show("Comanda nu a fost trimisă.");
                return false;
            }

            orderData = orderInfo.GetOrderData();

            if (string.IsNullOrWhiteSpace(orderData.Nume) ||
                string.IsNullOrWhiteSpace(orderData.Adresa) ||
                string.IsNullOrWhiteSpace(orderData.Telefon) ||
                string.IsNullOrWhiteSpace(orderData.Email))
            {
                MessageBox.Show("Comanda nu a fost completată.");
                return false;
            }
                var comanda = new Comanda
                {
                    Nume = orderData.Nume,
                    Adresa = orderData.Adresa,
                    Telefon = orderData.Telefon,
                    Email = orderData.Email,
                    DataComenzii = DateTime.Now,
                    Produse = AppState.GetProduse()
                };
                AppState.AdaugaComanda(comanda);
            user.DateLivrare = orderData;
            return true;
        }

        public bool PlaseazaComanda(MainForm.User user, OrderData orderData)
        {
            if (cartForm == null)
                cartForm = new CartForm(_mainForm);

            cartForm.SetProduse(AppState.GetProduse());
            List<Produs> produse = cartForm.GetProduse();

            if (produse.Count == 0)
            {
                MessageBox.Show("Nu există produse selectate pentru comandă.");
                return false;
            }

            var comanda = new Comanda(produse, orderData.Nume, orderData.Adresa, orderData.Telefon, orderData.Email);
            AppState.AdaugaComanda(comanda);
            user.Comenzi.Add(comanda);

            b.Items.Add($"Comanda din {comanda.DataComenzii.ToShortDateString()} - {comanda.Produse.Count} produse");
            foreach (var produs in comanda.Produse)
            {
                b.Items.Add($"Produs: {produs.Nume}, Preț: {produs.Pret} RON");
            }

            lblComenzi.Text = "Comanda a fost plasată cu succes!";
            return true;
        }

        private  string GetTipCont(MainForm.User utilizator)
        {
            if (utilizator is AngajatSenior) return "Angajat Senior";
            if (utilizator is AngajatJunior) return "Angajat Junior";
            if (utilizator is Manager) return "Manager";
            if (utilizator is Angajat) return "Angajat";
            return "Client";  // Default pentru client
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
        public List<Comanda> GetComenziUtilizator()
        {
            var utilizator = AppState.UtilizatorCurent;
            return utilizator?.Comenzi ?? new List<Comanda>();
        }

        private void back_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
