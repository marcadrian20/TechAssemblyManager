using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class AccountForm : Form
    {
        private MainForm mainForm;
        private ProductManagerBLL productManagerBLL;
        private UserManagerBLL userManagerBLL;
        private OrderManagerBLL orderManagerBLL;
        private User currentUser;

        private Label lblUser;
        private Label lblAccountType;
        private ListBox lstOrders;
        private Button btnLogoutButton;
        private Button btnRefreshOrders;

        public AccountForm(MainForm mainForm, ProductManagerBLL productManagerBLL, UserManagerBLL userManagerBLL, OrderManagerBLL orderManagerBLL)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.productManagerBLL = productManagerBLL;
            this.userManagerBLL = userManagerBLL;
            this.orderManagerBLL = orderManagerBLL;
            this.currentUser = SessionManager.LoggedInUser;

            this.Text = "Contul Meu";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblUser = new Label { Text = $"Utilizator: {currentUser?.userName ?? "Guest"}", AutoSize = true, Top = 20, Left = 20 };
            lblAccountType = new Label { Text = $"Tip cont: {userManagerBLL.GetAccountType(currentUser)}", AutoSize = true, Top = 50, Left = 20 };
            lstOrders = new ListBox { Top = 90, Left = 20, Width = 440, Height = 250 };
            btnRefreshOrders = new Button { Text = "Reîncarcă comenzi", Top = 350, Left = 20, Width = 180 };
            btnLogoutButton = new Button { Text = "Delogare", Top = 400, Left = 20, Width = 180 };

            btnRefreshOrders.Click += async (s, e) => await LoadOrdersAsync();
            btnLogoutButton.Click += BtnLogout_Click;

            this.Controls.Add(lblUser);
            this.Controls.Add(lblAccountType);
            this.Controls.Add(lstOrders);
            this.Controls.Add(btnRefreshOrders);
            this.Controls.Add(btnLogoutButton);

            this.FormClosing += AccountForm_FormClosing;
            this.Load += async (s, e) => await LoadOrdersAsync();
        }

        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
                mainForm.Show();
        }

        private async Task LoadOrdersAsync()
        {
            lstOrders.Items.Clear();
            if (currentUser == null)
            {
                lstOrders.Items.Add("Nu sunteți autentificat.");
                return;
            }

            var orders = await orderManagerBLL.GetOrdersByClientAsync(currentUser.userName);
            if (orders == null || orders.Count == 0)
            {
                lstOrders.Items.Add("Nu există comenzi plasate.");
                return;
            }

            foreach (var order in orders)
            {
                string dateText = order.OrderDate?.ToShortDateString() ?? "N/A";
                lstOrders.Items.Add($"ID: {order.OrderId} | Data: {dateText} | Status: {order.OrderStatus} | Total: {order.TotalCost} RON");

            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.LoggedInUser = null;
            MessageBox.Show("Delogat cu succes!");
            this.Hide();
            mainForm.Show();
        }
    }
}