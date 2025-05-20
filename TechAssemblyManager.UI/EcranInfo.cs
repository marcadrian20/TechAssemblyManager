using System;
using System.Drawing;
using System.Windows.Forms;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class EcranInfo : Form
    {
        private Product produs;
        private MainForm mainForm;
        public EcranInfo(Product produs)
        {
            mainForm=new MainForm();
            InitializeComponent();
            this.produs = produs;
            this.Text = "Detalii Produs";
            this.Size = new Size(400, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.FormClosing += EcranInfo_FormClosing;
            AfiseazaDetalii();
        }
        private void EcranInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(mainForm!=null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        private void AfiseazaDetalii()
        {
            Font fontTitlu = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontText = new Font("Segoe UI", 11);

            // PictureBox pic = new PictureBox
            // {
            //     Image = produs.Imagine,
            //     SizeMode = PictureBoxSizeMode.Zoom,
            //     Size = new Size(300, 250),
            //     Location = new Point(50, 30),
            //     BorderStyle = BorderStyle.FixedSingle
            // };

            Label lblname = new Label
            {
                Text = produs.name,
                Font = fontTitlu,
                ForeColor = Color.FromArgb(33, 33, 33),
                Location = new Point(50, 300),
                AutoSize = true
            };

            Label lblPret = new Label
            {
                Text = $"Preț: {produs.price} RON",
                Font = fontText,
                ForeColor = Color.FromArgb(66, 66, 66),
                Location = new Point(50, 340),
                AutoSize = true
            };

            Button btnInchide = new Button
            {
                Text = "Închide",
                Font = fontText,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(50, 400),
                Size = new Size(100, 40)
            };

            btnInchide.FlatAppearance.BorderSize = 0;
            btnInchide.Click += (s, e) => this.Close();

            // this.Controls.Add(pic);
            this.Controls.Add(lblname);
            this.Controls.Add(lblPret);
            this.Controls.Add(btnInchide);
        }
    }
}
