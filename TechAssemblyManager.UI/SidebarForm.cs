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
    public partial class SidebarForm: Form
    {
        private MainForm mainForm;
        public SidebarForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(200, Screen.PrimaryScreen.WorkingArea.Height);
            this.Location = new Point(0, 0);
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.TopMost = true;
            this.FormClosing += SidebarForm_FormClosing;
            InitializeSidebarButtons();
        }
        private void SidebarForm_FormClosing(object sender, EventArgs e)
        {
           if(mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        private void InitializeSidebarButtons()
        {
            var btnDashboard = CreateSidebarButton("Dashboard", (s, e) =>
            {
                MessageBox.Show("Ai ales Dashboard");
            });

            var btnOrders = CreateSidebarButton("Comenzi", (s, e) =>
            {
                MessageBox.Show("Ai ales Comenzi");
            });

            var btnSettings = CreateSidebarButton("Setări", (s, e) =>
            {
                MessageBox.Show("Ai ales Setări");
            });

            var btnClose = CreateSidebarButton("Închide", (s, e) =>
            {
                this.Close();
            });

            this.Controls.Add(btnClose);
            this.Controls.Add(btnSettings);
            this.Controls.Add(btnOrders);
            this.Controls.Add(btnDashboard);
        }

        private Button CreateSidebarButton(string text, EventHandler clickHandler)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Dock = DockStyle.Top;
            btn.Height = 50;
            btn.FlatStyle = FlatStyle.Flat;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(45, 45, 48);
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += clickHandler;
            return btn;
        }
    }
}
