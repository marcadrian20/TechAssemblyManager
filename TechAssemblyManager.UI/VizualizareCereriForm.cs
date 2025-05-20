using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class VizualizareCereriForm : Form
    {
        private ListBox lstCereri;
        private Button btnRezolva;
        private MainForm mainForm;
        private OrderManagerBLL orderManagerBLL;
        private User currentUser;
        private List<ServiceRequest> serviceRequests = new List<ServiceRequest>();

        public VizualizareCereriForm(User user, MainForm mainForm, OrderManagerBLL orderManagerBLL)
        {
            if (mainForm == null)
                throw new ArgumentNullException(nameof(mainForm), "MainForm cannot be null.");

            this.mainForm = mainForm;
            this.orderManagerBLL = orderManagerBLL;
            this.currentUser = user;
            this.Text = "Cereri Service";
            this.Size = new System.Drawing.Size(600, 400);

            lstCereri = new ListBox { Left = 10, Top = 10, Width = 560, Height = 300 };
            btnRezolva = new Button { Text = "Marchează ca rezolvată", Left = 10, Top = 320, Width = 200 };

            btnRezolva.Click += async (s, e) =>
            {
                if (lstCereri.SelectedIndex == -1)
                {
                    MessageBox.Show("Selectează o cerere.");
                    return;
                }
                var selectedRequest = serviceRequests[lstCereri.SelectedIndex];
                bool result = await orderManagerBLL.UpdateServiceRequestStatusAsync(selectedRequest.ServiceRequestId, "Rezolvată", currentUser);
                if (result)
                {
                    MessageBox.Show("Cererea a fost marcată ca rezolvată!");
                    await RefreshCereriAsync();
                }
                else
                {
                    MessageBox.Show("Eroare la actualizarea cererii.");
                }
            };

            this.FormClosing += VizualizareCereriForm_FormClosing;
            this.Controls.Add(lstCereri);
            this.Controls.Add(btnRezolva);

            _ = RefreshCereriAsync();
        }

        private void VizualizareCereriForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }

        private async Task RefreshCereriAsync()
        {
            lstCereri.Items.Clear();
            serviceRequests = await orderManagerBLL.GetAllServiceRequestsAsync();
            foreach (var c in serviceRequests)
            {
                lstCereri.Items.Add($"ID: {c.ServiceRequestId} | Client: {c.CustomerUserName} | Status: {c.Status} | Data: {c.RequestDate.ToShortDateString()} | Descriere: {c.ProblemDescription}");
            }
        }
    }
}