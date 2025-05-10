using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public class ServiceRequestsForm : Form
    {
        private ListView listView;
        private Button btnUpdateStatus;
        private MainForm mainForm;
        public ServiceRequestsForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.Text = "Istoric Cereri Service";
            this.Size = new Size(600, 400);

            listView = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new Point(10, 10),
                Size = new Size(560, 300)
            };

            listView.Columns.Add("Data", 150);
            listView.Columns.Add("Descriere", 250);
            listView.Columns.Add("Status", 150);

            btnUpdateStatus = new Button
            {
                Text = "Actualizează Statusul",
                Location = new Point(10, 320),
                Width = 200
            };

            btnUpdateStatus.Click += BtnUpdateStatus_Click;
            this.FormClosing += ServiceRequestsForm_FormClosing;
            Controls.Add(listView);
            Controls.Add(btnUpdateStatus);

            LoadCereri();
        }
        private void ServiceRequestsForm_FormClosing(object sender, EventArgs e)
        {
            if(mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        private void LoadCereri()
        {
            listView.Items.Clear();
            var cereri = AppState.GetCereriService();

            foreach (var cerere in cereri)
            {
                var item = new ListViewItem(cerere.DataProgramare.ToShortDateString());
                item.SubItems.Add(cerere.Descriere);
                item.SubItems.Add(cerere.Status);
                listView.Items.Add(item);
            }
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selectează o cerere.");
                return;
            }

            var item = listView.SelectedItems[0];
            var descriere = item.SubItems[1].Text;

            var cereri = AppState.GetCereriService();
            var cerere = cereri.Find(c => c.Descriere == descriere);

            if (cerere != null)
            {
                var statusNou = Prompt.ShowDialog("Introduceți noul status:", "Actualizează Status");
                cerere.Status = statusNou;
                LoadCereri();
            }
        }

        // Helper pentru input simplu:
        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 150,
                    Text = caption
                };
                Label textLabel = new Label() { Left = 10, Top = 10, Text = text };
                TextBox textBox = new TextBox() { Left = 10, Top = 30, Width = 360 };
                Button confirmation = new Button() { Text = "OK", Left = 280, Width = 90, Top = 60 };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.ShowDialog();
                return textBox.Text;
            }
        }
    }
}
