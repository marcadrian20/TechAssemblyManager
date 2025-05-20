using System;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class CerereServiceForm : Form
    {
        private DateTimePicker dtpData;
        private TextBox txtDescriere;
        private Button btnTrimite;
        private MainForm mainForm;
        private OrderManagerBLL orderManagerBLL;
        private User currentUser;

        public CerereServiceForm(MainForm mainForm, OrderManagerBLL orderManagerBLL, User currentUser)
        {
            this.mainForm = mainForm;
            this.orderManagerBLL = orderManagerBLL;
            this.currentUser = currentUser;
            this.Text = "Trimite cerere service";
            this.Size = new System.Drawing.Size(400, 250);

            Label lblData = new Label { Text = "Data programării:", Top = 20, Left = 20 };
            dtpData = new DateTimePicker { Top = 40, Left = 20, Width = 300 };

            Label lblDescriere = new Label { Text = "Descriere problemă:", Top = 80, Left = 20 };
            txtDescriere = new TextBox { Top = 100, Left = 20, Width = 300, Height = 60, Multiline = true };

            btnTrimite = new Button { Text = "Trimite cerere", Top = 180, Left = 20 };
            btnTrimite.Click += BtnTrimite_Click;

            this.FormClosing += CerereServiceForm_FormClosing;
            this.Controls.AddRange(new Control[] { lblData, dtpData, lblDescriere, txtDescriere, btnTrimite });
        }

        private void CerereServiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }

        private async void BtnTrimite_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescriere.Text))
            {
                MessageBox.Show("Completează descrierea.");
                return;
            }
            if (currentUser == null)
            {
                MessageBox.Show("Trebuie să fii autentificat.");
                return;
            }

            var cerere = new ServiceRequest
            {
                ServiceRequestId = Guid.NewGuid().ToString(),
                CustomerUserName = currentUser.userName,
                ProblemDescription = txtDescriere.Text,
                RequestDate = DateTime.Now,
                ScheduledDate = dtpData.Value.Date,
                Status = "Requested"
            };

            bool result = await orderManagerBLL.PlaceServiceRequestAsync(cerere, currentUser);
            if (result)
            {
                MessageBox.Show("Cererea a fost trimisă.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Eroare la trimiterea cererii.");
            }
        }
    }
}