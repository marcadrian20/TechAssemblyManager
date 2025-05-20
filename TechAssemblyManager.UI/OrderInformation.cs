using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class OrderInformation : Form
    {
        private Panel panelContainer;
        private MainForm _mainForm;
        private ProductManagerBLL productManagerBLL;
        private UserManagerBLL userManagerBLL;
        private OrderManagerBLL orderManagerBLL;
        private FlowLayoutPanel infoPanel;
        private User currentUser;

        public OrderInformation(MainForm mainForm, ProductManagerBLL productManagerBLL, UserManagerBLL userManagerBLL, OrderManagerBLL orderManagerBLL, User currentUser)
        {
            InitializeComponent();
            this.Text = "Order Information";
            _mainForm = mainForm;
            this.productManagerBLL = productManagerBLL;
            this.userManagerBLL = userManagerBLL;
            this.orderManagerBLL = orderManagerBLL;
            this.currentUser = currentUser;

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
            panelContainer.Controls.Add(infoPanel);

            this.FormClosing += OrderInformation_FormClosing;
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

        private void AddInfoFields()
        {
            infoPanel.Controls.Clear();
            string[] labels = { "Nume:", "Adresă:", "Telefon:", "Email:" };

            foreach (string label in labels)
            {
                var rowPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    Margin = new Padding(5)
                };

                Label lbl = new Label
                {
                    Text = label,
                    Width = 80,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                TextBox txt = new TextBox
                {
                    Name = "txt" + label.Replace(":", "").Replace("ă", "a"),
                    Width = 200
                };

                rowPanel.Controls.Add(lbl);
                rowPanel.Controls.Add(txt);
                infoPanel.Controls.Add(rowPanel);
            }
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
            AddInfoFields();
            CenterControls();
        }

        private void OrderInformation_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }

        // Example: Place order button click
        private async void BtnPlaceOrder_Click(object sender, EventArgs e)
        {
            var orderData = GetOrderData();
            // Validate fields
            if (string.IsNullOrWhiteSpace(orderData.Nume) ||
                string.IsNullOrWhiteSpace(orderData.Adresa) ||
                string.IsNullOrWhiteSpace(orderData.Telefon) ||
                string.IsNullOrWhiteSpace(orderData.Email))
            {
                MessageBox.Show("Vă rugăm să completați toate câmpurile înainte de a trimite comanda.",
                                "Câmpuri incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Here you would build an Order object and call orderManagerBLL.PlaceOrderAsync
            // Example:
            // var order = new Order { ... };
            // bool result = await orderManagerBLL.PlaceOrderAsync(order, currentUser);

            // For now, just show a confirmation
            MessageBox.Show("Comanda a fost plasată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            _mainForm.Show();
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

    // Helper class for order data
    public class OrderData
    {
        public string Nume { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
    }
}