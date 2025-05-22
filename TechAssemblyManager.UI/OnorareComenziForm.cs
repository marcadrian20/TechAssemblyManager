using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;
namespace TechAssemblyManager.UI
{
    public partial class OnorareComenziForm : Form
    {
        private ListBox lstComenzi;
        private Button btnModificaStatus;
        private ComboBox cmbStatusNou;
        private Label lblStatusNou;
        private MainForm mainForm;
        // public CartForm cartForm;
        // private ProductViewerForm prvf;
        // private AccountForm accountForm;
        private List<Order> orders = new List<Order>();
        private ProductManagerBLL productManagerBLL;
        private OrderManagerBLL orderManagerBLL;
        private UserManagerBLL userManagerBLL;
        private User currentUser;

        public OnorareComenziForm(MainForm mainForm, OrderManagerBLL orderManagerBLL)
        {
            if (mainForm == null || mainForm.Instance == null)
            {
                throw new ArgumentNullException(nameof(mainForm), "MainForm or its Instance cannot be null.");
            }
            if (mainForm == null)
                throw new ArgumentNullException(nameof(mainForm), "MainForm cannot be null.");

            this.mainForm = mainForm;
            this.orderManagerBLL = orderManagerBLL;
            this.currentUser = SessionManager.LoggedInUser;
            // this.prvf = new ProductViewerForm(this);
            this.Text = "Onorare Comenzi";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            lstComenzi = new ListBox() { Width = 450, Height = 200, Top = 20, Left = 30 };
            lblStatusNou = new Label() { Text = "Status nou:", Top = 240, Left = 20 };
            cmbStatusNou = new ComboBox() { Top = 260, Left = 20, Width = 200 };
            cmbStatusNou.Items.AddRange(new string[] { "În așteptare", "În curs", "Finalizată" });

            btnModificaStatus = new Button() { Text = "Actualizează Status", Top = 300, Left = 20, Width = 200 };
            btnModificaStatus.Click += BtnModificaStatus_Click;
            this.Controls.Add(lstComenzi);
            this.Controls.Add(lblStatusNou);
            this.Controls.Add(cmbStatusNou);
            this.Controls.Add(btnModificaStatus);
            this.FormClosing += OnorareComenziForm_FormClosing;
            IncarcaComenziAsync();
        }
        private void OnorareComenziForm_FormClosing(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        private async Task IncarcaComenziAsync()
        {
            lstComenzi.Items.Clear();
            orders = await orderManagerBLL.GetAllOrdersAsync();
            foreach (var order in orders)
            {
                string dateText = order.OrderDate?.ToShortDateString() ?? "N/A";

                lstComenzi.Items.Add($"ID: {order.OrderId} | Client: {order.ClientUserName} | Status: {order.OrderStatus} | Data: {order.OrderDate} | Total: {order.TotalCost} RON");
            }

        }

        private async void BtnModificaStatus_Click(object sender, EventArgs e)
        {
            if (lstComenzi.SelectedIndex == -1)
            {
                MessageBox.Show("Selectează o comandă.");
                return;
            }

            string statusNou = cmbStatusNou.SelectedItem as string;
            lstComenzi.Items.Add($"Statusul comenzii a fost actualizat la: {statusNou}");
            if (string.IsNullOrEmpty(statusNou))
            {
                MessageBox.Show("Selectează un status nou.");
                return;
            }

            var selectedOrder = orders[lstComenzi.SelectedIndex];
            bool result = await orderManagerBLL.UpdateOrderStatusAsync(selectedOrder.OrderId, statusNou, currentUser);

            if (result)
            {
                MessageBox.Show("Status actualizat cu succes!");
                await IncarcaComenziAsync();
            }
            else
            {
                MessageBox.Show("Eroare la actualizarea statusului.");
            }
            // Aici actualizezi statusul în obiectul real
            MessageBox.Show("Status actualizat la: " + statusNou);
        }
    }
}

