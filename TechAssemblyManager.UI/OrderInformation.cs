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

namespace TechAssemblyManager.UI
{
    public partial class OrderInformation : Form
    {
        private System.Windows.Forms.Panel panelContainer;
        private MainForm _mainForm;
        private ProductViewerForm productViewerForm;
        private ProductManagerBLL productManagerBLL;
        private FlowLayoutPanel infoPanel; // Adăugat pentru a reține panoul de informații
        private AccountForm _accountForm;
        public object Instance => this;
        public bool EsteAutentificat { get; set; } = false;
        public OrderInformation(MainForm mainForm)
        {
            InitializeComponent();
            this.Text = "OrderInformation.cs";
            _mainForm = mainForm;
            panelContainer = new Panel();
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.BackColor = Color.White;
            this.Controls.Add(panelContainer);

            infoPanel = new FlowLayoutPanel();
            infoPanel.AutoSize = true;
            infoPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            infoPanel.Anchor = AnchorStyles.None;
            infoPanel.FlowDirection = FlowDirection.TopDown;
            infoPanel.WrapContents = false;
            infoPanel.AutoScroll = true;
            this.FormClosing += OrderInformation_FormClosing;
            panelContainer.Controls.Add(infoPanel);
            this.Load += OrderInformation_Load;
            this.Resize += OrderInformation_Resize;
        }
        private void OrderInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_mainForm != null && !_mainForm.IsDisposed)
            {
                _mainForm.Show();
            }
        }
        private void LoadFormInPanel(Form frm)
        {
            panelContainer.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(frm);
            panelContainer.Tag = frm;
            frm.Show();
            panelContainer.BringToFront();
        }
        private void AdaugaCampuriInformatii()
        {
            if (infoPanel.Controls.Count > 0)
                return;

            string[] etichete = { "Nume:", "Adresă:", "Telefon:", "Email:" };

            foreach (string eticheta in etichete)
            {
                var panelRand = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    Margin = new Padding(5)
                };

                Label lbl = new Label
                {
                    Text = eticheta,
                    Width = 80,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                TextBox txt = new TextBox
                {
                    Name = "txt" + eticheta.Replace(":", "").Replace("ă", "a"),
                    Width = 200
                };

                panelRand.Controls.Add(lbl);
                panelRand.Controls.Add(txt);
                infoPanel.Controls.Add(panelRand);
            }
        }

        public bool ClickATrimis { get; private set; } = false;

        public void Trimitere_Click(object sender, EventArgs e)
        {
            ClickATrimis = true;
            foreach (Control control in infoPanel.Controls)
            {
                foreach (Control subControl in control.Controls)
                {
                    if (subControl is TextBox txt && string.IsNullOrWhiteSpace(txt.Text))
                    {
                        MessageBox.Show("Vă rugăm să completați toate câmpurile înainte de a trimite comanda.",
                                        "Câmpuri incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                }
            }
            Panel panelMesaj = new Panel
            {
                Size = new Size(300, 150),
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 150) / 2)
            };

            Label lblMesaj = new Label
            {
                Text = "COMANDA PLASATĂ",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };

            Button btnIesire = new Button
            {
                Text = "IEȘIRE",
                Size = new Size(100, 30),
                Location = new Point(30, 80)
            };
            btnIesire.Click += (s, args) =>
            {
                CartForm cartForm = new CartForm(_mainForm, productManagerBLL, productViewerForm);
                cartForm.Show();
                this.Hide();
            };

            Button btnContinuare = new Button
            {
                Text = "CONTINUAȚI VIZUALIZARE",
                Size = new Size(160, 30),
                Location = new Point(140, 80)
            };
            btnContinuare.Click += (s, args) =>
            {
                this.Controls.Remove(panelMesaj);
                MainForm main = new MainForm();
                main.Show();
                this.Hide();
            };

            panelMesaj.Controls.Add(lblMesaj);
            panelMesaj.Controls.Add(btnIesire);
            panelMesaj.Controls.Add(btnContinuare);

            this.Controls.Add(panelMesaj);
            panelMesaj.BringToFront();
        }

        private void CenterControls()
        {
            if (infoPanel.Controls.Count == 0)
                return;

            int totalHeight = infoPanel.Controls.Cast<Control>().Sum(c => c.Height + c.Margin.Vertical);
            int totalWidth = infoPanel.Controls.Cast<Control>().Max(c => c.Width + c.Margin.Horizontal);

            infoPanel.Size = new Size(totalWidth, totalHeight);
            infoPanel.Left = (this.ClientSize.Width - infoPanel.Width) / 2;
            infoPanel.Top = (this.ClientSize.Height - infoPanel.Height) / 2;
        }

        private void OrderInformation_Load(object sender, EventArgs e)
        {
            AdaugaCampuriInformatii();
            Trimitere.Enabled = EsteAutentificat;
            CenterControls();
        }

        private void OrderInformation_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void Account_Click(object sender, EventArgs e)
        {
            AccountForm accountForm = new AccountForm(_mainForm, this, productViewerForm, productManagerBLL);
            accountForm.ShowDialog();
            this.Hide();
        }
        public OrderData GetOrderData()
        {
            var data = new OrderData();

            foreach (Control control in infoPanel.Controls)
            {
                foreach (Control subControl in control.Controls)
                {
                    if (subControl is TextBox txt)
                    {
                        if (txt.Name.Contains("Nume")) data.Nume = txt.Text;
                        else if (txt.Name.Contains("Adresa")) data.Adresa = txt.Text;
                        else if (txt.Name.Contains("Telefon")) data.Telefon = txt.Text;
                        else if (txt.Name.Contains("Email")) data.Email = txt.Text;
                    }
                }
            }
            return data;
        }
    }
}