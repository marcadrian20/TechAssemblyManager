using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class OrderInformation : Form
    {
        private System.Windows.Forms.Panel panelContainer;
        private MainForm _mainForm;
        private ProductViewerForm productViewerForm;
        private FlowLayoutPanel infoPanel; // Adăugat pentru a reține panoul de informații

        public object Instance => this;

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
            infoPanel.Dock = DockStyle.Fill;
            infoPanel.FlowDirection = FlowDirection.TopDown;
            infoPanel.WrapContents = false;
            infoPanel.AutoScroll = true;

            panelContainer.Controls.Add(infoPanel);

            this.Load += OrderInformation_Load;
            this.Resize += OrderInformation_Resize;
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
            panelContainer.BringToFront(); // <- asta te asigură că e în față
        }
        private void AdaugaCampuriInformatii()
        {
            // Verificăm dacă deja au fost adăugate câmpurile
            if (infoPanel.Controls.OfType<TextBox>().Any())
                return;

            string[] etichete = { "Nume:", "Adresă:", "Telefon:", "Email:" };
            int labelWidth = 80;
            int textBoxWidth = 200;

            foreach (string eticheta in etichete)
            {
                Label lbl = new Label();
                lbl.Text = eticheta;
                lbl.Width = labelWidth;

                TextBox txt = new TextBox();
                txt.Name = "txt" + eticheta.Replace(":", "").Replace("ă", "a");
                txt.Width = textBoxWidth;

                infoPanel.Controls.Add(lbl);
                infoPanel.Controls.Add(txt);
            }

            Button btnTrimitere = new Button();
            btnTrimitere.Text = "Trimite Comanda";
            btnTrimitere.Width = 150;
            btnTrimitere.Click += Trimitere_Click;

            infoPanel.Controls.Add(btnTrimitere);
        }


        private void Trimitere_Click(object sender, EventArgs e)
        {
            foreach (Control control in infoPanel.Controls)
            {
                if (control is TextBox txt && string.IsNullOrWhiteSpace(txt.Text))
                {
                    MessageBox.Show("Vă rugăm să completați toate câmpurile înainte de a trimite comanda.",
                                    "Câmpuri incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Dacă toate câmpurile sunt completate, continuă cu plasarea comenzii
            Panel panelMesaj = new Panel();
            panelMesaj.Size = new Size(300, 150);
            panelMesaj.BackColor = Color.LightGray;
            panelMesaj.BorderStyle = BorderStyle.FixedSingle;
            panelMesaj.Location = new Point((this.ClientSize.Width - panelMesaj.Width) / 2,
                                            (this.ClientSize.Height - panelMesaj.Height) / 2);
            panelMesaj.BringToFront();

            Label lblMesaj = new Label();
            lblMesaj.Text = "COMANDA PLASATĂ";
            lblMesaj.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblMesaj.TextAlign = ContentAlignment.MiddleCenter;
            lblMesaj.Dock = DockStyle.Top;
            lblMesaj.Height = 50;

            Button btnIesire = new Button();
            btnIesire.Text = "IEȘIRE";
            btnIesire.Size = new Size(100, 30);
            btnIesire.Location = new Point(30, 80);
            btnIesire.Click += (s, args) =>
            {
                CartForm cartForm = new CartForm(_mainForm, productViewerForm);
                cartForm.Show();
                this.Hide();
            };

            Button btnContinuare = new Button();
            btnContinuare.Text = "CONTINUAȚI VIZUALIZARE";
            btnContinuare.Size = new Size(160, 30);
            btnContinuare.Location = new Point(140, 80);
            btnContinuare.Click += (s, args) =>
            {
                this.Controls.Remove(panelMesaj); 
                MainForm main = new MainForm();
                main.Show();
                this.Hide();

            };

            // Adaugă în panel
            panelMesaj.Controls.Add(lblMesaj);
            panelMesaj.Controls.Add(btnIesire);
            panelMesaj.Controls.Add(btnContinuare);

            // Adaugă panel-ul în form
            this.Controls.Add(panelMesaj);
            panelMesaj.BringToFront();
        }

        private void CenterControls()
        {
            // Centrare ListBox
            Introducereinformatii.Left = (this.ClientSize.Width - Introducereinformatii.Width) / 2;
            Introducereinformatii.Top = (this.ClientSize.Height - Introducereinformatii.Height) / 2 - 40;

            // Buton sub ListBox
            Trimitere.Left = (this.ClientSize.Width - Trimitere.Width) / 2;
            Trimitere.Top = Introducereinformatii.Bottom + 10;
        }

        private void OrderInformation_Load(object sender, EventArgs e)
        {
            CenterControls();
            AdaugaCampuriInformatii();
        }

        private void OrderInformation_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }
    }
}
